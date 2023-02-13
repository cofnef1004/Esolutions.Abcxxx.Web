(function () {
    $(function () {
        var _vilagesService = abp.services.app.vilages;

        var _$vilageInformationForm = $('form[name=VilageInformationsForm]');
        _$vilageInformationForm.validate();

		
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	    

        function save(successCallback) {
            if (!_$vilageInformationForm.valid()) {
                return;
            }

            var vilage = _$vilageInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _vilagesService.createOrEdit(
				vilage
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditVilageModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$vilageInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Vilages";
            });
        });
        
        $('#saveAndNewBtn').click(function(){
            save(function(){
                if (!$('input[name=id]').val()) {//if it is create page
                   clearForm();
                }
            });
        });
    });
})();