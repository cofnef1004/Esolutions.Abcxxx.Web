(function ($) {
    app.modals.StadiumLookupTableModal = function () {

        var _modalManager;

        var _clubsService = abp.services.app.clubs;
        var _$stadiumTable = $('#StadiumTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$stadiumTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clubsService.getAllStadiumForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#StadiumTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" + app.localize('Select') + "' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#StadiumTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getStadium() {
            dataTable.ajax.reload();
        }

        $('#GetStadiumButton').click(function (e) {
            e.preventDefault();
            getStadium();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getStadium();
            }
        });

    };
})(jQuery);

