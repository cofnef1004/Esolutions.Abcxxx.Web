(function () {
    $(function () {
        var _playersService = abp.services.app.players;

        var _$playerInformationForm = $('form[name=PlayerInformationsForm]');
        _$playerInformationForm.validate();

		        var _PlayerclubLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Players/ClubLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Players/_PlayerClubLookupTableModal.js',
            modalClass: 'ClubLookupTableModal'
        });        var _PlayernationLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Players/NationLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Players/_PlayerNationLookupTableModal.js',
            modalClass: 'NationLookupTableModal'
        });
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	            $('#OpenClubLookupTableButton').click(function () {

            var player = _$playerInformationForm.serializeFormToObject();

            _PlayerclubLookupTableModal.open({ id: player.clubId, displayName: player.clubMACLB }, function (data) {
                _$playerInformationForm.find('input[name=clubMACLB]').val(data.displayName); 
                _$playerInformationForm.find('input[name=clubId]').val(data.id); 
            });
        });
		
		$('#ClearClubMACLBButton').click(function () {
                _$playerInformationForm.find('input[name=clubMACLB]').val(''); 
                _$playerInformationForm.find('input[name=clubId]').val(''); 
        });
		
        $('#OpenNationLookupTableButton').click(function () {

            var player = _$playerInformationForm.serializeFormToObject();

            _PlayernationLookupTableModal.open({ id: player.nationId, displayName: player.nationmaqg }, function (data) {
                _$playerInformationForm.find('input[name=nationmaqg]').val(data.displayName); 
                _$playerInformationForm.find('input[name=nationId]').val(data.id); 
            });
        });
		
		$('#ClearNationmaqgButton').click(function () {
                _$playerInformationForm.find('input[name=nationmaqg]').val(''); 
                _$playerInformationForm.find('input[name=nationId]').val(''); 
        });
		


        function save(successCallback) {
            if (!_$playerInformationForm.valid()) {
                return;
            }
            if ($('#Player_ClubId').prop('required') && $('#Player_ClubId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Club')));
                return;
            }
            if ($('#Player_NationId').prop('required') && $('#Player_NationId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Nation')));
                return;
            }

            var player = _$playerInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _playersService.createOrEdit(
				player
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditPlayerModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$playerInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Players";
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