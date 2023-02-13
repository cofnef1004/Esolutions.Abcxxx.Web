(function () {
    $(function () {

        var _$bpCoachTable = $('#BpCoachTable');
        var _bpCoachService = abp.services.app.bpCoach;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.BpCoach.Create'),
            edit: abp.auth.hasPermission('Pages.BpCoach.Edit'),
            'delete': abp.auth.hasPermission('Pages.BpCoach.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/BpCoach/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BpCoach/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditBpCoachModal'
                });
                   

		 var _viewBpCoachModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/BpCoach/ViewbpCoachModal',
            modalClass: 'ViewBpCoachModal'
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

        var dataTable = _$bpCoachTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _bpCoachService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#BpCoachTableFilter').val(),
					mAHLVFilter: $('#MAHLVFilterId').val(),
					tENHLVFilter: $('#TENHLVFilterId').val(),
					dIENTHOAIFilter: $('#DIENTHOAIFilterId').val(),
					mAQUOCGIAFilter: $('#MAQUOCGIAFilterId').val()
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
                                    _viewBpCoachModal.open({ id: data.record.bpCoach.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.bpCoach.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteBpCoach(data.record.bpCoach);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "bpCoach.mahlv",
						 name: "mahlv"   
					},
					{
						targets: 3,
						 data: "bpCoach.tenhlv",
						 name: "tenhlv"   
					},
					{
						targets: 4,
						 data: "bpCoach.dienthoai",
						 name: "dienthoai"   
					},
					{
						targets: 5,
						 data: "bpCoach.maquocgia",
						 name: "maquocgia"   
					}
            ]
        });

        function getBpCoach() {
            dataTable.ajax.reload();
        }

        function deleteBpCoach(bpCoach) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _bpCoachService.delete({
                            id: bpCoach.id
                        }).done(function () {
                            getBpCoach(true);
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

        $('#CreateNewBpCoachButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditBpCoachModalSaved', function () {
            getBpCoach();
        });

		$('#GetBpCoachButton').click(function (e) {
            e.preventDefault();
            getBpCoach();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getBpCoach();
		  }
		});
		
		
		
    });
})();
