(function () {
    $(function () {
        var _stadiumsService = abp.services.app.stadiums;

        var _$stadiumInformationForm = $('form[name=StadiumInformationsForm]');
        _$stadiumInformationForm.validate();

		
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	    

        function save(successCallback) {
            if (!_$stadiumInformationForm.valid()) {
                return;
            }

            var stadium = _$stadiumInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _stadiumsService.createOrEdit(
				stadium
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditStadiumModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$stadiumInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Stadiums";
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