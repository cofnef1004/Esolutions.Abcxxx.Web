(function () {
    $(function () {

        var _$listHLVsTable = $('#ListHLVsTable');
        var _listHLVsService = abp.services.app.listHLVs;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ListHLVs.Create'),
            edit: abp.auth.hasPermission('Pages.ListHLVs.Edit'),
            'delete': abp.auth.hasPermission('Pages.ListHLVs.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/ListHLVs/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ListHLVs/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditListHLVModal'
                });
                   

		 var _viewListHLVModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/ListHLVs/ViewlistHLVModal',
            modalClass: 'ViewListHLVModal'
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

        var dataTable = _$listHLVsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _listHLVsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ListHLVsTableFilter').val(),
					mahlvFilter: $('#MahlvFilterId').val(),
					mACLBFilter: $('#MACLBFilterId').val(),
					vAITROFilter: $('#VAITROFilterId').val()
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
                                    _viewListHLVModal.open({ id: data.record.listHLV.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.listHLV.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteListHLV(data.record.listHLV);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "listHLV.mahlv",
						 name: "mahlv"   
					},
					{
						targets: 3,
						 data: "listHLV.maclb",
						 name: "maclb"   
					},
					{
						targets: 4,
						 data: "listHLV.vaitro",
						 name: "vaitro"   
					}
            ]
        });

        function getListHLVs() {
            dataTable.ajax.reload();
        }

        function deleteListHLV(listHLV) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _listHLVsService.delete({
                            id: listHLV.id
                        }).done(function () {
                            getListHLVs(true);
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

        $('#CreateNewListHLVButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditListHLVModalSaved', function () {
            getListHLVs();
        });

		$('#GetListHLVsButton').click(function (e) {
            e.preventDefault();
            getListHLVs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getListHLVs();
		  }
		});
		
		
		
    });
})();
