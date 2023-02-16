(function () {
    $(function () {

        var _$rankingsTable = $('#RankingsTable');
        var _rankingsService = abp.services.app.rankings;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Rankings.Create'),
            edit: abp.auth.hasPermission('Pages.Rankings.Edit'),
            'delete': abp.auth.hasPermission('Pages.Rankings.Delete')
        };

               

		 var _viewRankingModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Rankings/ViewrankingModal',
            modalClass: 'ViewRankingModal'
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

        var dataTable = _$rankingsTable.DataTable({
            paging: false,
            serverSide: false,
            processing: false,
            listAction: {
                ajaxFunction: _rankingsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#RankingsTableFilter').val(),
					minnamFilter: $('#MinnamFilterId').val(),
					maxnamFilter: $('#MaxnamFilterId').val(),
					minvongFilter: $('#MinvongFilterId').val(),
					maxvongFilter: $('#MaxvongFilterId').val(),
					mintranFilter: $('#MintranFilterId').val(),
					maxtranFilter: $('#MaxtranFilterId').val(),
					minthangFilter: $('#MinthangFilterId').val(),
					maxthangFilter: $('#MaxthangFilterId').val(),
					minhoaFilter: $('#MinhoaFilterId').val(),
					maxhoaFilter: $('#MaxhoaFilterId').val(),
					minthuaFilter: $('#MinthuaFilterId').val(),
					maxthuaFilter: $('#MaxthuaFilterId').val(),
					minhieusoFilter: $('#MinhieusoFilterId').val(),
					maxhieusoFilter: $('#MaxhieusoFilterId').val(),
					mindiemFilter: $('#MindiemFilterId').val(),
					maxdiemFilter: $('#MaxdiemFilterId').val(),
					clubTENCLBFilter: $('#ClubTENCLBFilterId').val()
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
/*                {
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
                                    window.location="/App/Rankings/ViewRanking/" + data.record.ranking.id;
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            window.location="/App/Rankings/CreateOrEdit/" + data.record.ranking.id;                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteRanking(data.record.ranking);
                            }
                        }]
                    }
                },*/
                    {
                        targets: 1,
                        data: "clubTENCLB",
                        name: "maclbFk.tenclb"
                    },
					{
						targets: 2,
						 data: "ranking.nam",
						 name: "nam"   
					},
					{
						targets: 3,
						 data: "ranking.vong",
						 name: "vong"   
					},
					{
						targets: 4,
						 data: "ranking.tran",
						 name: "tran"   
					},
					{
						targets: 5,
						 data: "ranking.thang",
						 name: "thang"   
					},
					{
						targets: 6,
						 data: "ranking.hoa",
						 name: "hoa"   
					},
					{
						targets: 7,
						 data: "ranking.thua",
						 name: "thua"   
					},
					{
						targets: 8,
						 data: "ranking.hieuso",
						 name: "hieuso"   
					},
					{
						targets: 9,
						 data: "ranking.diem",
						 name: "diem"   
					}
            ]
        });

      /*  function getRankings() {
            dataTable.ajax.reload();
        }

        function deleteRanking(ranking) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _rankingsService.delete({
                            id: ranking.id
                        }).done(function () {
                            getRankings(true);
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
            _rankingsService
                .getRankingsToExcel({
				filter : $('#RankingsTableFilter').val(),
					minnamFilter: $('#MinnamFilterId').val(),
					maxnamFilter: $('#MaxnamFilterId').val(),
					minvongFilter: $('#MinvongFilterId').val(),
					maxvongFilter: $('#MaxvongFilterId').val(),
					mintranFilter: $('#MintranFilterId').val(),
					maxtranFilter: $('#MaxtranFilterId').val(),
					minthangFilter: $('#MinthangFilterId').val(),
					maxthangFilter: $('#MaxthangFilterId').val(),
					minhoaFilter: $('#MinhoaFilterId').val(),
					maxhoaFilter: $('#MaxhoaFilterId').val(),
					minthuaFilter: $('#MinthuaFilterId').val(),
					maxthuaFilter: $('#MaxthuaFilterId').val(),
					minhieusoFilter: $('#MinhieusoFilterId').val(),
					maxhieusoFilter: $('#MaxhieusoFilterId').val(),
					mindiemFilter: $('#MindiemFilterId').val(),
					maxdiemFilter: $('#MaxdiemFilterId').val(),
					clubTENCLBFilter: $('#ClubTENCLBFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditRankingModalSaved', function () {
            getRankings();
        });

		$('#GetRankingsButton').click(function (e) {
            e.preventDefault();
            getRankings();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getRankings();
		  }
		});
		*/
		
		
    });
})();
