(function () {
    $(function () {
        var _rankingsService = abp.services.app.rankings;

        var _$rankingInformationForm = $('form[name=RankingInformationsForm]');
        _$rankingInformationForm.validate();

		        var _RankingclubLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Rankings/ClubLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Rankings/_RankingClubLookupTableModal.js',
            modalClass: 'ClubLookupTableModal'
        });
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	            $('#OpenClubLookupTableButton').click(function () {

            var ranking = _$rankingInformationForm.serializeFormToObject();

            _RankingclubLookupTableModal.open({ id: ranking.maclb, displayName: ranking.clubTENCLB }, function (data) {
                _$rankingInformationForm.find('input[name=clubTENCLB]').val(data.displayName); 
                _$rankingInformationForm.find('input[name=maclb]').val(data.id); 
            });
        });
		
		$('#ClearClubTENCLBButton').click(function () {
                _$rankingInformationForm.find('input[name=clubTENCLB]').val(''); 
                _$rankingInformationForm.find('input[name=maclb]').val(''); 
        });
		


        function save(successCallback) {
            if (!_$rankingInformationForm.valid()) {
                return;
            }
            if ($('#Ranking_maclb').prop('required') && $('#Ranking_maclb').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Club')));
                return;
            }

            var ranking = _$rankingInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _rankingsService.createOrEdit(
				ranking
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditRankingModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$rankingInformationForm[0].reset();
        }
        
        $('#saveBtn').click(function(){
            save(function(){
                window.location="/App/Rankings";
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