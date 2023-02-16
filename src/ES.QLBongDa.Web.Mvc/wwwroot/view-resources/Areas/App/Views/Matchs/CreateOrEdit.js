(function () {
    $(function () {
        var _matchsService = abp.services.app.matchs;

        var _$matchInformationForm = $('form[name=MatchInformationsForm]');
        _$matchInformationForm.validate();

		        var _MatchclubLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Matchs/ClubLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Matchs/_MatchClubLookupTableModal.js',
            modalClass: 'ClubLookupTableModal'
        });        var _MatchstadiumLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Matchs/StadiumLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Matchs/_MatchStadiumLookupTableModal.js',
            modalClass: 'StadiumLookupTableModal'
        });
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	            $('#OpenClubLookupTableButton').click(function () {

            var match = _$matchInformationForm.serializeFormToObject();

            _MatchclubLookupTableModal.open({ id: match.maclb1, displayName: match.clubTENCLB }, function (data) {
                _$matchInformationForm.find('input[name=clubTENCLB]').val(data.displayName); 
                _$matchInformationForm.find('input[name=maclb1]').val(data.id); 
            });
        });
		
		$('#ClearClubTENCLBButton').click(function () {
                _$matchInformationForm.find('input[name=clubTENCLB]').val(''); 
                _$matchInformationForm.find('input[name=maclb1]').val(''); 
        });
		
        $('#OpenClub2LookupTableButton').click(function () {

            var match = _$matchInformationForm.serializeFormToObject();

            _MatchclubLookupTableModal.open({ id: match.maclb2, displayName: match.clubTENCLB2 }, function (data) {
                _$matchInformationForm.find('input[name=clubTENCLB2]').val(data.displayName); 
                _$matchInformationForm.find('input[name=maclb2]').val(data.id); 
            });
        });
		
		$('#ClearClubTENCLB2Button').click(function () {
                _$matchInformationForm.find('input[name=clubTENCLB2]').val(''); 
                _$matchInformationForm.find('input[name=maclb2]').val(''); 
        });
		
        $('#OpenStadiumLookupTableButton').click(function () {

            var match = _$matchInformationForm.serializeFormToObject();

            _MatchstadiumLookupTableModal.open({ id: match.masan, displayName: match.stadiumTensan }, function (data) {
                _$matchInformationForm.find('input[name=stadiumTensan]').val(data.displayName); 
                _$matchInformationForm.find('input[name=masan]').val(data.id); 
            });
        });
		
		$('#ClearStadiumTensanButton').click(function () {
                _$matchInformationForm.find('input[name=stadiumTensan]').val(''); 
                _$matchInformationForm.find('input[name=masan]').val(''); 
        });
		


        function save(successCallback) {
            if (!_$matchInformationForm.valid()) {
                return;
            }
            if ($('#Match_Maclb1').prop('required') && $('#Match_Maclb1').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Club')));
                return;
            }
            if ($('#Match_Maclb2').prop('required') && $('#Match_Maclb2').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Club')));
                return;
            }
            if ($('#Match_Masan').prop('required') && $('#Match_Masan').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Stadium')));
                return;
            }

            var match = _$matchInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _matchsService.createOrEdit(
				match
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditMatchModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$matchInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Matchs";
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