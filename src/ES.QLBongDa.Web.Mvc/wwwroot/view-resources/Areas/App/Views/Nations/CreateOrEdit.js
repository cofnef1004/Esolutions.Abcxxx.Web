(function () {
    $(function () {
        var _nationsService = abp.services.app.nations;

        var _$nationInformationForm = $('form[name=NationInformationsForm]');
        _$nationInformationForm.validate();

		
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	    

        function save(successCallback) {
            if (!_$nationInformationForm.valid()) {
                return;
            }

            var nation = _$nationInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _nationsService.createOrEdit(
				nation
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditNationModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$nationInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Nations";
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
})(jQuery);