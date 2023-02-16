(function () {
    $(function () {
        var _managersService = abp.services.app.managers;

        var _$managerInformationForm = $('form[name=ManagerInformationsForm]');
        _$managerInformationForm.validate();

		        var _ManagernationLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Managers/NationLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Managers/_ManagerNationLookupTableModal.js',
            modalClass: 'NationLookupTableModal'
        });
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	            $('#OpenNationLookupTableButton').click(function () {

            var manager = _$managerInformationForm.serializeFormToObject();

            _ManagernationLookupTableModal.open({ id: manager.nationId, displayName: manager.nationtenqg }, function (data) {
                _$managerInformationForm.find('input[name=nationtenqg]').val(data.displayName); 
                _$managerInformationForm.find('input[name=nationId]').val(data.id); 
            });
        });
		
		$('#ClearNationtenqgButton').click(function () {
                _$managerInformationForm.find('input[name=nationtenqg]').val(''); 
                _$managerInformationForm.find('input[name=nationId]').val(''); 
        });
		


        function save(successCallback) {
            if (!_$managerInformationForm.valid()) {
                return;
            }
            if ($('#Manager_NationId').prop('required') && $('#Manager_NationId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Nation')));
                return;
            }

            var manager = _$managerInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _managersService.createOrEdit(
				manager
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditManagerModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$managerInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Managers";
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