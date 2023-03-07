(function ($) {
    app.modals.CreateOrEditPlayerModal = function () {

        var _playersService = abp.services.app.players;

        var _modalManager;
        var _$playerInformationForm = null;

		        var _PlayerclubLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Players/ClubLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Players/_PlayerClubLookupTableModal.js',
            modalClass: 'ClubLookupTableModal'
        });        var _PlayernationLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Players/NationLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Players/_PlayerNationLookupTableModal.js',
            modalClass: 'NationLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$playerInformationForm = _modalManager.getModal().find('form[name=PlayerInformationsForm]');
            _$playerInformationForm.validate();
        };

		          $('#OpenClubLookupTableButton').click(function () {

            var player = _$playerInformationForm.serializeFormToObject();

            _PlayerclubLookupTableModal.open({ id: player.clubId, displayName: player.clubMACLB }, function (data) {
                _$playerInformationForm.find('input[name=clubMACLB]').val(data.displayName); 
                _$playerInformationForm.find('input[name=clubId]').val(data.id); 
            });
        });
		
		$('#ClearClubMACLBButton').click(function () {
                _$playerInformationForm.find('input[name=clubMACLB]').val(''); 
                _$playerInformationForm.find('input[name=clubId]').val(''); 
        });
		
        $('#OpenNationLookupTableButton').click(function () {

            var player = _$playerInformationForm.serializeFormToObject();

            _PlayernationLookupTableModal.open({ id: player.nationId, displayName: player.nationmaqg }, function (data) {
                _$playerInformationForm.find('input[name=nationmaqg]').val(data.displayName); 
                _$playerInformationForm.find('input[name=nationId]').val(data.id); 
            });
        });
		
		$('#ClearNationmaqgButton').click(function () {
                _$playerInformationForm.find('input[name=nationmaqg]').val(''); 
                _$playerInformationForm.find('input[name=nationId]').val(''); 
        });
		


        this.save = function () {
            if (!_$playerInformationForm.valid()) {
                return;
            }
            if ($('#Player_ClubId').prop('required') && $('#Player_ClubId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Club')));
                return;
            }
            if ($('#Player_NationId').prop('required') && $('#Player_NationId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Nation')));
                return;
            }

            

            var player = _$playerInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _playersService.createOrEdit(
				player
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditPlayerModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);