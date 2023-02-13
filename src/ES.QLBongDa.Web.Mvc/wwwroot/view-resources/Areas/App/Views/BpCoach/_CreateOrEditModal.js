(function ($) {
    app.modals.CreateOrEditBpCoachModal = function () {

        var _bpCoachService = abp.services.app.bpCoach;

        var _modalManager;
        var _$bpCoachInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$bpCoachInformationForm = _modalManager.getModal().find('form[name=BpCoachInformationsForm]');
            _$bpCoachInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$bpCoachInformationForm.valid()) {
                return;
            }

            

            var bpCoach = _$bpCoachInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _bpCoachService.createOrEdit(
				bpCoach
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditBpCoachModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);