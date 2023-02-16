(function () {
    $(function () {
        var _hlVsService = abp.services.app.hlVs;

        var _$hlvInformationForm = $('form[name=HLVInformationsForm]');
        _$hlvInformationForm.validate();

		        var _HLVnationLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/HLVs/NationLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/HLVs/_HLVNationLookupTableModal.js',
            modalClass: 'NationLookupTableModal'
        });
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	            $('#OpenNationLookupTableButton').click(function () {

            var hlv = _$hlvInformationForm.serializeFormToObject();

            _HLVnationLookupTableModal.open({ id: hlv.nationId, displayName: hlv.nationtenqg }, function (data) {
                _$hlvInformationForm.find('input[name=nationtenqg]').val(data.displayName); 
                _$hlvInformationForm.find('input[name=nationId]').val(data.id); 
            });
        });
		
		$('#ClearNationtenqgButton').click(function () {
                _$hlvInformationForm.find('input[name=nationtenqg]').val(''); 
                _$hlvInformationForm.find('input[name=nationId]').val(''); 
        });
		


        function save(successCallback) {
            if (!_$hlvInformationForm.valid()) {
                return;
            }
            if ($('#HLV_NationId').prop('required') && $('#HLV_NationId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Nation')));
                return;
            }

            var hlv = _$hlvInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _hlVsService.createOrEdit(
				hlv
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditHLVModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$hlvInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/HLVs";
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