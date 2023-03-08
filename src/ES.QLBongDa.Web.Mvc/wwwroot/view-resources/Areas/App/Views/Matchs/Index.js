(function () {
    $(function () {

        var _$matchsTable = $('#MatchsTable');
        var _matchsService = abp.services.app.matchs;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Matchs.Create'),
            edit: abp.auth.hasPermission('Pages.Matchs.Edit'),
            'delete': abp.auth.hasPermission('Pages.Matchs.Delete')
        };

               

		 var _viewMatchModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Matchs/ViewmatchModal',
            modalClass: 'ViewMatchModal'
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

        var dataTable = _$matchsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _matchsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#MatchsTableFilter').val(),
					minNamFilter: $('#MinNamFilterId').val(),
					maxNamFilter: $('#MaxNamFilterId').val(),
					minVongFilter: $('#MinVongFilterId').val(),
					maxVongFilter: $('#MaxVongFilterId').val(),
					ketquaFilter: $('#KetquaFilterId').val(),
					clubTENCLBFilter: $('#ClubTENCLBFilterId').val(),
					clubTENCLB2Filter: $('#ClubTENCLB2FilterId').val(),
					stadiumTensanFilter: $('#StadiumTensanFilterId').val()
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
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            window.location="/App/Matchs/CreateOrEdit/" + data.record.match.id;                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteMatch(data.record.match);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "match.nam",
						 name: "nam"   
					},
					{
						targets: 3,
						 data: "match.vong",
						 name: "vong"   
                    },
                    {
                        targets: 4,
                        data: "clubTENCLB",
                        name: "maclb1Fk.tenclb"
                    },
					{
						targets: 5,
                        data:/* "match.ketqua" + "match.ketqua1" + */"match.ketqua",
                        name: "ketqua"
					},
					{
						targets: 6,
						 data: "clubTENCLB2" ,
						 name: "maclb2Fk.tenclb" 
					},
					{
						targets: 7,
						 data: "stadiumTensan" ,
						 name: "masanFk.tensan" 
					}
            ]
        });

        function getMatchs() {
            dataTable.ajax.reload();
        }

        function deleteMatch(match) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _matchsService.delete({
                            id: match.id
                        }).done(function () {
                            getMatchs(true);
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
            _matchsService
                .getMatchsToExcel({
				filter : $('#MatchsTableFilter').val(),
					minNamFilter: $('#MinNamFilterId').val(),
					maxNamFilter: $('#MaxNamFilterId').val(),
					minVongFilter: $('#MinVongFilterId').val(),
					maxVongFilter: $('#MaxVongFilterId').val(),
					ketquaFilter: $('#KetquaFilterId').val(),
					clubTENCLBFilter: $('#ClubTENCLBFilterId').val(),
					clubTENCLB2Filter: $('#ClubTENCLB2FilterId').val(),
					stadiumTensanFilter: $('#StadiumTensanFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditMatchModalSaved', function () {
            getMatchs();
        });

		$('#GetMatchsButton').click(function (e) {
            e.preventDefault();
            getMatchs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getMatchs();
		  }
		});
		
		
		
    });
})();
