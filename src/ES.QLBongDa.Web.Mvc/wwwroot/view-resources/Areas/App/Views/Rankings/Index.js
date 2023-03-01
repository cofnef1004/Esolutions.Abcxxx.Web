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

        var dataTable = _$rankingsTable.DataTable({
            paging: false,
            serverSide: false,
            processing: false,
            listAction: {
                ajaxFunction: _rankingsService.getAll,
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

        function getRankings() {
            dataTable.ajax.reload();
        }
        $('#RefreshBXHButton').click(function (e) {
            e.preventDefault();
            abp.notify.info(app.localize('Tổng Hợp Trận Đấu Thành Công'));
            getRankings();
        });
		
    });
})();
