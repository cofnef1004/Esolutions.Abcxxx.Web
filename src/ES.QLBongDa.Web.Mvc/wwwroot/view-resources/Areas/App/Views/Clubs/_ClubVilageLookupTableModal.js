(function ($) {
    app.modals.VilageLookupTableModal = function () {

        var _modalManager;

        var _clubsService = abp.services.app.clubs;
        var _$vilageTable = $('#VilageTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$vilageTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clubsService.getAllVilageForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#VilageTableFilter').val()
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

        $('#VilageTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getVilage() {
            dataTable.ajax.reload();
        }

        $('#GetVilageButton').click(function (e) {
            e.preventDefault();
            getVilage();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getVilage();
            }
        });

    };
})(jQuery);

