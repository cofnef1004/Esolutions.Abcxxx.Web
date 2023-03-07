(function ($) {
    app.modals.CreateOrEditClubModal = function () {

        var _clubsService = abp.services.app.clubs;

        var _modalManager;
        var _$clubInformationForm = null;

		        var _ClubstadiumLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Clubs/StadiumLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Clubs/_ClubStadiumLookupTableModal.js',
            modalClass: 'StadiumLookupTableModal'
        });        var _ClubvilageLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Clubs/VilageLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Clubs/_ClubVilageLookupTableModal.js',
            modalClass: 'VilageLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$clubInformationForm = _modalManager.getModal().find('form[name=ClubInformationsForm]');
            _$clubInformationForm.validate();
        };

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
		


        this.save = function () {
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
            
            
            
			
			 _modalManager.setBusy(true);
			 _clubsService.createOrEdit(
				club
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditClubModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);