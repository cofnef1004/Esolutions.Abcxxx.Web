(function () {
    $(function () {

        var _$playersTable = $('#PlayersTable');
        var _playersService = abp.services.app.players;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Players.Create'),
            edit: abp.auth.hasPermission('Pages.Players.Edit'),
            'delete': abp.auth.hasPermission('Pages.Players.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Players/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Players/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditPlayerModal'
                });
                   

		 var _viewPlayerModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Players/ViewplayerModal',
            modalClass: 'ViewPlayerModal'
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

        var dataTable = _$playersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _playersService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#PlayersTableFilter').val(),
					hotenFilter: $('#HotenFilterId').val(),
					vitriFilter: $('#VitriFilterId').val(),
					minsoaoFilter: $('#MinsoaoFilterId').val(),
					maxsoaoFilter: $('#MaxsoaoFilterId').val(),
					clubMACLBFilter: $('#ClubMACLBFilterId').val(),
					nationmaqgFilter: $('#NationmaqgFilterId').val()
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
                                    _viewPlayerModal.open({ id: data.record.player.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.player.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deletePlayer(data.record.player);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "player.hoten",
						 name: "hoten"   
					},
					{
						targets: 3,
						 data: "player.vitri",
						 name: "vitri"   
					},
					{
						targets: 4,
						 data: "player.soao",
						 name: "soao"   
					},
					{
						targets: 5,
						 data: "clubMACLB" ,
						 name: "clubFk.maclb" 
					},
					{
						targets: 6,
						 data: "nationmaqg" ,
						 name: "nationFk.maqg" 
					}
            ]
        });

        function getPlayers() {
            dataTable.ajax.reload();
        }

        function deletePlayer(player) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _playersService.delete({
                            id: player.id
                        }).done(function () {
                            getPlayers(true);
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

        $('#CreateNewPlayerButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _playersService
                .getPlayersToExcel({
				filter : $('#PlayersTableFilter').val(),
					hotenFilter: $('#HotenFilterId').val(),
					vitriFilter: $('#VitriFilterId').val(),
					minsoaoFilter: $('#MinsoaoFilterId').val(),
					maxsoaoFilter: $('#MaxsoaoFilterId').val(),
					clubMACLBFilter: $('#ClubMACLBFilterId').val(),
					nationmaqgFilter: $('#NationmaqgFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditPlayerModalSaved', function () {
            getPlayers();
        });

		$('#GetPlayersButton').click(function (e) {
            e.preventDefault();
            getPlayers();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getPlayers();
		  }
		});
		
		
		
    });
})();
