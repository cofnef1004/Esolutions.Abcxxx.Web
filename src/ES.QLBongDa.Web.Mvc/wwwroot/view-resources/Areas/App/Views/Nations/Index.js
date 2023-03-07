﻿(function () {
    $(function () {

        var _$nationsTable = $('#NationsTable');
        var _nationsService = abp.services.app.nations;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Nations.Create'),
            edit: abp.auth.hasPermission('Pages.Nations.Edit'),
            'delete': abp.auth.hasPermission('Pages.Nations.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Nations/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Nations/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditNationModal'
                });
                   

		 var _viewNationModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Nations/ViewnationModal',
            modalClass: 'ViewNationModal'
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

        var dataTable = _$nationsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _nationsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#NationsTableFilter').val(),
					maqgFilter: $('#maqgFilterId').val(),
					tenqgFilter: $('#tenqgFilterId').val()
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
                                    _viewNationModal.open({ id: data.record.nation.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.nation.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteNation(data.record.nation);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "nation.maqg",
						 name: "maqg"   
					},
					{
						targets: 3,
						 data: "nation.tenqg",
						 name: "tenqg"   
					}
            ]
        });

        function getNations() {
            dataTable.ajax.reload();
        }

        function deleteNation(nation) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _nationsService.delete({
                            id: nation.id
                        }).done(function () {
                            getNations(true);
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

        $('#CreateNewNationButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _nationsService
                .getNationsToExcel({
				filter : $('#NationsTableFilter').val(),
					maqgFilter: $('#maqgFilterId').val(),
					tenqgFilter: $('#tenqgFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditNationModalSaved', function () {
            getNations();
        });

		$('#GetNationsButton').click(function (e) {
            e.preventDefault();
            getNations();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getNations();
		  }
		});
		
		
		
    });
})();
