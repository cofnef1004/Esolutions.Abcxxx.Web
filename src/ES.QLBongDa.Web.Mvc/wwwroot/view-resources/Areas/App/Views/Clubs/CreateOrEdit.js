(function () {
    $(function () {
        var _clubsService = abp.services.app.clubs;

        var _$clubInformationForm = $('form[name=ClubInformationsForm]');
        _$clubInformationForm.validate();

		        var _ClubstadiumLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Clubs/StadiumLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Clubs/_ClubStadiumLookupTableModal.js',
            modalClass: 'StadiumLookupTableModal'
        });        var _ClubvilageLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Clubs/VilageLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Clubs/_ClubVilageLookupTableModal.js',
            modalClass: 'VilageLookupTableModal'
        });
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	            $('#OpenStadiumLookupTableButton').click(function () {

            var club = _$clubInformationForm.serializeFormToObject();

            _ClubstadiumLookupTableModal.open({ id: club.stadiumId, displayName: club.stadiumTensan }, function (data) {
                _$clubInformationForm.find('input[name=stadiumTensan]').val(data.displayName); 
                _$clubInformationForm.find('input[name=stadiumId]').val(data.id); 
            });
        });
		
		$('#ClearStadiumTensanButton').click(function () {
                _$clubInformationForm.find('input[name=stadiumTensan]').val(''); 
                _$clubInformationForm.find('input[name=stadiumId]').val(''); 
        });
		
        $('#OpenVilageLookupTableButton').click(function () {

            var club = _$clubInformationForm.serializeFormToObject();

            _ClubvilageLookupTableModal.open({ id: club.vilageId, displayName: club.vilagetentinh }, function (data) {
                _$clubInformationForm.find('input[name=vilagetentinh]').val(data.displayName); 
                _$clubInformationForm.find('input[name=vilageId]').val(data.id); 
            });
        });
		
		$('#ClearVilagetentinhButton').click(function () {
                _$clubInformationForm.find('input[name=vilagetentinh]').val(''); 
                _$clubInformationForm.find('input[name=vilageId]').val(''); 
        });
		


        function save(successCallback) {
            if (!_$clubInformationForm.valid()) {
                return;
            }
            if ($('#Club_StadiumId').prop('required') && $('#Club_StadiumId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Stadium')));
                return;
            }
            if ($('#Club_VilageId').prop('required') && $('#Club_VilageId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Vilage')));
                return;
            }

            var club = _$clubInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _clubsService.createOrEdit(
				club
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditClubModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$clubInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Clubs";
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