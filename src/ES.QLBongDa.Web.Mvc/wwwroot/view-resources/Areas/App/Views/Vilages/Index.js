(function () {
    $(function () {

        var _$vilagesTable = $('#VilagesTable');
        var _vilagesService = abp.services.app.vilages;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Vilages.Create'),
            edit: abp.auth.hasPermission('Pages.Vilages.Edit'),
            'delete': abp.auth.hasPermission('Pages.Vilages.Delete')
        };

               

		 var _viewVilageModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Vilages/ViewvilageModal',
            modalClass: 'ViewVilageModal'
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

        var dataTable = _$vilagesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _vilagesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#VilagesTableFilter').val(),
					matinhFilter: $('#matinhFilterId').val(),
					tentinhFilter: $('#tentinhFilterId').val()
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
                                    window.location="/App/Vilages/ViewVilage/" + data.record.vilage.id;
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            window.location="/App/Vilages/CreateOrEdit/" + data.record.vilage.id;                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteVilage(data.record.vilage);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "vilage.matinh",
						 name: "matinh"   
					},
					{
						targets: 3,
						 data: "vilage.tentinh",
						 name: "tentinh"   
					}
            ]
        });

        function getVilages() {
            dataTable.ajax.reload();
        }

        function deleteVilage(vilage) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _vilagesService.delete({
                            id: vilage.id
                        }).done(function () {
                            getVilages(true);
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
            _vilagesService
                .getVilagesToExcel({
				filter : $('#VilagesTableFilter').val(),
					matinhFilter: $('#matinhFilterId').val(),
					tentinhFilter: $('#tentinhFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditVilageModalSaved', function () {
            getVilages();
        });

		$('#GetVilagesButton').click(function (e) {
            e.preventDefault();
            getVilages();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getVilages();
		  }
		});
		
		
		
    });
})();
