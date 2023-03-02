(function ($) {
    app.modals.CreateOrEditListHLVModal = function () {

        var _listHLVsService = abp.services.app.listHLVs;

        var _modalManager;
        var _$listHLVInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$listHLVInformationForm = _modalManager.getModal().find('form[name=ListHLVInformationsForm]');
            _$listHLVInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$listHLVInformationForm.valid()) {
                return;
            }

            

            var listHLV = _$listHLVInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _listHLVsService.createOrEdit(
				listHLV
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditListHLVModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);