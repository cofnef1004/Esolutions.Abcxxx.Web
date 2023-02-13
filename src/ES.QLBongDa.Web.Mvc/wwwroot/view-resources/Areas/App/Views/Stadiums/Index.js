(function () {
    $(function () {

        var _$stadiumsTable = $('#StadiumsTable');
        var _stadiumsService = abp.services.app.stadiums;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Stadiums.Create'),
            edit: abp.auth.hasPermission('Pages.Stadiums.Edit'),
            'delete': abp.auth.hasPermission('Pages.Stadiums.Delete')
        };

               

		 var _viewStadiumModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Stadiums/ViewstadiumModal',
            modalClass: 'ViewStadiumModal'
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

        var dataTable = _$stadiumsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _stadiumsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#StadiumsTableFilter').val(),
					masanFilter: $('#MasanFilterId').val(),
					tensanFilter: $('#TensanFilterId').val()
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
                                    window.location="/App/Stadiums/ViewStadium/" + data.record.stadium.id;
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            window.location="/App/Stadiums/CreateOrEdit/" + data.record.stadium.id;                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteStadium(data.record.stadium);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "stadium.masan",
						 name: "masan"   
					},
					{
						targets: 3,
						 data: "stadium.tensan",
						 name: "tensan"   
					}
            ]
        });

        function getStadiums() {
            dataTable.ajax.reload();
        }

        function deleteStadium(stadium) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _stadiumsService.delete({
                            id: stadium.id
                        }).done(function () {
                            getStadiums(true);
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
            _stadiumsService
                .getStadiumsToExcel({
				filter : $('#StadiumsTableFilter').val(),
					masanFilter: $('#MasanFilterId').val(),
					tensanFilter: $('#TensanFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditStadiumModalSaved', function () {
            getStadiums();
        });

		$('#GetStadiumsButton').click(function (e) {
            e.preventDefault();
            getStadiums();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getStadiums();
		  }
		});
		
		
		
    });
})();
