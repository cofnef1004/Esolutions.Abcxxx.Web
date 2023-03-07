(function ($) {
    app.modals.CreateOrEditNationModal = function () {

        var _nationsService = abp.services.app.nations;

        var _modalManager;
        var _$nationInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$nationInformationForm = _modalManager.getModal().find('form[name=NationInformationsForm]');
            _$nationInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$nationInformationForm.valid()) {
                return;
            }

            

            var nation = _$nationInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _nationsService.createOrEdit(
				nation
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditNationModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);