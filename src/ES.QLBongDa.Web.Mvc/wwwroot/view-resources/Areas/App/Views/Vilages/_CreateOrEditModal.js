(function ($) {
    app.modals.CreateOrEditVilageModal = function () {

        var _vilagesService = abp.services.app.vilages;

        var _modalManager;
        var _$vilageInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$vilageInformationForm = _modalManager.getModal().find('form[name=VilageInformationsForm]');
            _$vilageInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$vilageInformationForm.valid()) {
                return;
            }

            

            var vilage = _$vilageInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _vilagesService.createOrEdit(
				vilage
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditVilageModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);