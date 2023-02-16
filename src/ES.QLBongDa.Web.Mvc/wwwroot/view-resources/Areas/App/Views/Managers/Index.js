(function () {
    $(function () {

        var _$managersTable = $('#ManagersTable');
        var _managersService = abp.services.app.managers;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Managers.Create'),
            edit: abp.auth.hasPermission('Pages.Managers.Edit'),
            'delete': abp.auth.hasPermission('Pages.Managers.Delete')
        };

               

		 var _viewManagerModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Managers/ViewmanagerModal',
            modalClass: 'ViewManagerModal'
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

        var dataTable = _$managersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _managersService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ManagersTableFilter').val(),
					mahlvFilter: $('#MahlvFilterId').val(),
					tenhlvFilter: $('#TenhlvFilterId').val(),
					minNamsinhFilter: $('#MinNamsinhFilterId').val(),
					maxNamsinhFilter: $('#MaxNamsinhFilterId').val(),
					nationtenqgFilter: $('#NationtenqgFilterId').val()
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
                                    window.location="/App/Managers/ViewManager/" + data.record.manager.id;
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            window.location="/App/Managers/CreateOrEdit/" + data.record.manager.id;                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteManager(data.record.manager);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "manager.mahlv",
						 name: "mahlv"   
					},
					{
						targets: 3,
						 data: "manager.tenhlv",
						 name: "tenhlv"   
					},
					{
						targets: 4,
						 data: "manager.namsinh",
						 name: "namsinh"   
					},
					{
						targets: 5,
						 data: "nationtenqg" ,
						 name: "nationFk.tenqg" 
					}
            ]
        });

        function getManagers() {
            dataTable.ajax.reload();
        }

        function deleteManager(manager) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _managersService.delete({
                            id: manager.id
                        }).done(function () {
                            getManagers(true);
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
            _managersService
                .getManagersToExcel({
				filter : $('#ManagersTableFilter').val(),
					mahlvFilter: $('#MahlvFilterId').val(),
					tenhlvFilter: $('#TenhlvFilterId').val(),
					minNamsinhFilter: $('#MinNamsinhFilterId').val(),
					maxNamsinhFilter: $('#MaxNamsinhFilterId').val(),
					nationtenqgFilter: $('#NationtenqgFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditManagerModalSaved', function () {
            getManagers();
        });

		$('#GetManagersButton').click(function (e) {
            e.preventDefault();
            getManagers();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getManagers();
		  }
		});
		
		
		
    });
})();
