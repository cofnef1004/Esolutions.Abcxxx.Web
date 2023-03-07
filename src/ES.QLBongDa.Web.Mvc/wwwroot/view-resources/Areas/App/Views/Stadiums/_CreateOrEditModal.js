(function ($) {
    app.modals.CreateOrEditStadiumModal = function () {

        var _stadiumsService = abp.services.app.stadiums;

        var _modalManager;
        var _$stadiumInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$stadiumInformationForm = _modalManager.getModal().find('form[name=StadiumInformationsForm]');
            _$stadiumInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$stadiumInformationForm.valid()) {
                return;
            }

            

            var stadium = _$stadiumInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _stadiumsService.createOrEdit(
				stadium
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditStadiumModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);