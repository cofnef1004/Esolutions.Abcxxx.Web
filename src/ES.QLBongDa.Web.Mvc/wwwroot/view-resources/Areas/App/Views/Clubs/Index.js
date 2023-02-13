(function () {
    $(function () {

        var _$clubsTable = $('#ClubsTable');
        var _clubsService = abp.services.app.clubs;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Clubs.Create'),
            edit: abp.auth.hasPermission('Pages.Clubs.Edit'),
            'delete': abp.auth.hasPermission('Pages.Clubs.Delete')
        };

               

		 var _viewClubModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Clubs/ViewclubModal',
            modalClass: 'ViewClubModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }
        
        var getMaxDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT23:59:59Z"); 
        }

        var dataTable = _$clubsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clubsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ClubsTableFilter').val(),
					mACLBFilter: $('#MACLBFilterId').val(),
					tENCLBFilter: $('#TENCLBFilterId').val(),
					stadiumTensanFilter: $('#StadiumTensanFilterId').val(),
					vilagetentinhFilter: $('#VilagetentinhFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
						{
                                text: app.localize('View'),
                                iconStyle: 'far fa-eye mr-2',
                                action: function (data) {
                                    window.location="/App/Clubs/ViewClub/" + data.record.club.id;
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            window.location="/App/Clubs/CreateOrEdit/" + data.record.club.id;                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteClub(data.record.club);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "club.maclb",
						 name: "maclb"   
					},
					{
						targets: 3,
						 data: "club.tenclb",
						 name: "tenclb"   
					},
					{
						targets: 4,
						 data: "stadiumTensan" ,
						 name: "stadiumFk.tensan" 
					},
					{
						targets: 5,
						 data: "vilagetentinh" ,
						 name: "vilageFk.tentinh" 
					}
            ]
        });

        function getClubs() {
            dataTable.ajax.reload();
        }

        function deleteClub(club) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _clubsService.delete({
                            id: club.id
                        }).done(function () {
                            getClubs(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

		$('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

                

		$('#ExportToExcelButton').click(function () {
            _clubsService
                .getClubsToExcel({
				filter : $('#ClubsTableFilter').val(),
					mACLBFilter: $('#MACLBFilterId').val(),
					tENCLBFilter: $('#TENCLBFilterId').val(),
					stadiumTensanFilter: $('#StadiumTensanFilterId').val(),
					vilagetentinhFilter: $('#VilagetentinhFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditClubModalSaved', function () {
            getClubs();
        });

		$('#GetClubsButton').click(function (e) {
            e.preventDefault();
            getClubs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getClubs();
		  }
		});
		
		
		
    });
})();
