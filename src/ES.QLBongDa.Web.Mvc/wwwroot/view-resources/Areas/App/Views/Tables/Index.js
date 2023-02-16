(function () {
    $(function () {

        var _$tablesTable = $('#TablesTable');
        var _tablesService = abp.services.app.tables;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Tables.Create'),
            edit: abp.auth.hasPermission('Pages.Tables.Edit'),
            'delete': abp.auth.hasPermission('Pages.Tables.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Tables/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Tables/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditTableModal'
                });
                   

		 var _viewTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Tables/ViewtableModal',
            modalClass: 'ViewTableModal'
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

        var dataTable = _$tablesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _tablesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#TablesTableFilter').val(),
					minnamFilter: $('#MinnamFilterId').val(),
					maxnamFilter: $('#MaxnamFilterId').val(),
					minvongFilter: $('#MinvongFilterId').val(),
					maxvongFilter: $('#MaxvongFilterId').val(),
					minsotranFilter: $('#MinsotranFilterId').val(),
					maxsotranFilter: $('#MaxsotranFilterId').val(),
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
                                    _viewTableModal.open({ id: data.record.table.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.table.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteTable(data.record.table);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "table.nam",
						 name: "nam"   
					},
					{
						targets: 3,
						 data: "table.vong",
						 name: "vong"   
					},
					{
						targets: 4,
						 data: "table.sotran",
						 name: "sotran"   
					},
					{
						targets: 5,
						 data: "table.thang",
						 name: "thang"   
					},
					{
						targets: 6,
						 data: "table.hoa",
						 name: "hoa"   
					},
					{
						targets: 7,
						 data: "table.thua",
						 name: "thua"   
					},
					{
						targets: 8,
						 data: "table.hieuso",
						 name: "hieuso"   
					},
					{
						targets: 9,
						 data: "table.diem",
						 name: "diem"   
					},
					{
						targets: 10,
						 data: "clubTENCLB" ,
						 name: "maclbFk.tenclb" 
					}
            ]
        });

        function getTables() {
            dataTable.ajax.reload();
        }

        function deleteTable(table) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _tablesService.delete({
                            id: table.id
                        }).done(function () {
                            getTables(true);
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

        $('#CreateNewTableButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditTableModalSaved', function () {
            getTables();
        });

		$('#GetTablesButton').click(function (e) {
            e.preventDefault();
            getTables();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getTables();
		  }
		});
		
		
		
    });
})();
