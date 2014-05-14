_3o3.GameEditor = new Class({
	Implements: [Events,Chain,Options],
	options:{
		winnerSelectorSelected:'images/basketball_x250.png',
		winnerSelectorNoneSelected:'images/basketball_light_gray_x250.png',
		winnerSelectorNotSelected:'images/basketball_light_gray_x250.png',
	},
	events:{
		show:'show',
		hide:'hide',
		confirmed:'confirmed',
		confirmCompleted:'confirmCompleted',
		showEditDisplay:'showEditDisplay',
		hideEditDisplay:'hideEditDisplay',
		showConfirmDisplay:'showConfirmDisplay',
		hideConfirmDisplay:'hideConfirmDisplay'
	},
	views:{
		edit:'edit',
		confirm:'confirm'
	},
	currentView:null,
	initialize: function(element, controller, options){
		this.setOptions(options);
		this.element = element;
		this.controller = controller;
		
		this.editDisplay = new Element('div',{'class':'_3o3_game_display__edit_display'});
		this.editDisplay.hide();
		this.editDisplay.teams=[];
		for(var i=0;i<2;i++){
			var teamDisplay = new Element('div',{'class':'_3o3_game_display__edit_display__team _3o3_game_display__edit_display__team_'+(i+1)});
			teamDisplay.teamName = new Element('div',{'text':'Team '+(i+1),'class':'_3o3_game_display__edit_display__team__name'});
			
			teamDisplay.score = new Element('div',{'text':'0','class':'_3o3_game_display__edit_display__team__score'});
			teamDisplay.score.container = new Element('div',{'class':'_3o3_game_display__edit_display__team__score__container'});			
			teamDisplay.score.container.grab(teamDisplay.score);
			
			teamDisplay.score.minus = new Element('button',{'text':'-1','class':'_3o3_game_display__edit_display__team__score__minus _3o3_game_display__edit_display__team__button'});
			teamDisplay.score.plus = new Element('button',{'text':'+1','class':'_3o3_game_display__edit_display__team__score__plus _3o3_game_display__edit_display__team__button'});
			teamDisplay.score.plusTwo = new Element('button',{'text':'+2','class':'_3o3_game_display__edit_display__team__score__plus_two _3o3_game_display__edit_display__team__button'});
			
			teamDisplay.score.minus.addEvent('click',this.changeTeamPoints.pass([teamDisplay,-1],this));
			teamDisplay.score.plus.addEvent('click',this.changeTeamPoints.pass([teamDisplay,1],this));
			teamDisplay.score.plusTwo.addEvent('click',this.changeTeamPoints.pass([teamDisplay,2],this));
			
			
			teamDisplay.fouls = new Element('div',{'text':'0','class':'_3o3_game_display__edit_display__team__fouls'});
			teamDisplay.fouls.container = new Element('div',{'class':'_3o3_game_display__edit_display__team__fouls__container'});			
			teamDisplay.fouls.container.grab(teamDisplay.fouls);
			
			teamDisplay.fouls.minus = new Element('button',{'text':'-1','class':'_3o3_game_display__edit_display__team__fouls__minus _3o3_game_display__edit_display__team__button'});
			teamDisplay.fouls.plus = new Element('button',{'text':'+1','class':'_3o3_game_display__edit_display__team__fouls__plus _3o3_game_display__edit_display__team__button'});
						
			teamDisplay.fouls.minus.addEvent('click',this.changeTeamFouls.pass([teamDisplay,-1],this));
			teamDisplay.fouls.plus.addEvent('click',this.changeTeamFouls.pass([teamDisplay,1],this));
			
			teamDisplay.fouls.label = new Element('div',{'text':'Fouls','class':'_3o3_game_display__edit_display__team__fouls__label'});
			
			teamDisplay.adopt(
				teamDisplay.teamName,
				teamDisplay.score.minus,
				teamDisplay.score.container,
				teamDisplay.score.plus,
				teamDisplay.score.plusTwo,
				teamDisplay.fouls.minus,
				teamDisplay.fouls.container,
				teamDisplay.fouls.plus,
				teamDisplay.fouls.label);
				
			this.editDisplay.teams.push(teamDisplay);
			this.editDisplay.grab(teamDisplay);
		}
				
		this.confirmDisplay = new Element('div',{'class':'_3o3_game_display__confirm_display'});
		this.confirmDisplay.hide();
		this.confirmDisplay.teams=[];
		for(var i=0;i<2;i++){
			var teamDisplay = new Element('div',{'class':'_3o3_game_display__confirm_display__team _3o3_game_display__edit_display__team_'+(i+1)});
			
			teamDisplay.winnerSelector = new Element('img',{'class':'_3o3_game_display__confirm_display__team__selector'});
			this.initializeWinnerSelector(teamDisplay.winnerSelector);
			teamDisplay.teamName = new Element('span',{'class':'_3o3_game_display__confirm_display__team__name'});
			
			teamDisplay.score = new Element('input',{'type':'text','class':'_3o3_game_display__confirm_display__team__score'});			
			teamDisplay.score.addEvent('keypress',this.handleNumericalTextBoxKeyPress.bind(this));
			
			teamDisplay.fouls = new Element('input',{'type':'text','class':'_3o3_game_display__confirm_display__team__fouls'});
			teamDisplay.fouls.addEvent('keypress',this.handleNumericalTextBoxKeyPress.bind(this));
			
			teamDisplay.adopt(
				teamDisplay.winnerSelector,
				teamDisplay.teamName,
				new Element('br'),
				teamDisplay.score,
				new Element('br'),
				teamDisplay.fouls);
				
			this.confirmDisplay.teams.push(teamDisplay);
			this.confirmDisplay.grab(teamDisplay);
		}
		this.confirmDisplay.confirmButton=new Element('button',{'html':'Confirm','class':'_3o3_game_display__confirm_display__confirm_button'});
		this.confirmDisplay.confirmButton.addEvent('click',this.confirm.bind(this));
		this.confirmDisplay.grab(this.confirmDisplay.confirmButton);
		
		this.element.adopt(this.editDisplay,this.confirmDisplay);
	},
	winnerSelectors:[],
	initializeWinnerSelector:function(selector,clickableElements){
		this.winnerSelectors.push(selector);
		selector.addEvent('click',this.handleWinnerSelectorClicked.pass(selector,this));
		if(typeof(clickableElements)=='array'){
			clickableElements.each(
				function(el){
					el.addEvent('click',this.handleWinnerSelectorClicked.pass(selector,this));
				}.bind(this));
		}
		this.setWinnerSelectorValue(selector,null,false);
	},
	resetWinnerSelectors:function(){
		this.winnerSelectors.each(
			function(selector){
				this.setWinnerSelectorValue(selector,null,false);
			}.bind(this));
	},
	handleWinnerSelectorClicked:function(selector){
		this.setWinnerSelectorValue(selector,!selector.selected);
	},
	setWinnerSelectorValue:function(selector,value,calculateOtherValues){
		var setSelectorValue=
			function(s,value){
				if(value==null){
					this.setWinnerSelectorImage(s,this.options.winnerSelectorNoneSelected);
					s.selected=false;
					return;
				}
				if(value==true)this.setWinnerSelectorImage(s,this.options.winnerSelectorSelected);
				else if(value==false)this.setWinnerSelectorImage(s,this.options.winnerSelectorNotSelected);
				s.selected=value;
			}.bind(this);
		
		if(value==false){
			var otherTeamIsWinner=this.winnerSelectors.some(
				function(s){
					return s!=selector&&s.selected;
				});
			if(!otherTeamIsWinner)return false;
		}
		
		setSelectorValue(selector,value);
		
		if(calculateOtherValues!=false){
			if(value==true){
				this.winnerSelectors.each(
					function(s){
						if(s!=selector)setSelectorValue(s,false);
					});
			}
			else{
				var someSelected = this.winnerSelectors.some(
					function(s){
						return s.selected;
					});
				if(!someSelected){
					this.winnerSelectors.each(
					function(s){
						setSelectorValue(s,null);
					});
				}
			}
		}
		return true;
	},
	setWinnerSelectorImage:function(selector,image){
		if(image==null)selector.hide();
		selector.set('src',image);
		if(image!=null)selector.show();
	},
	firstTeamToTiedValue:null,
	changeTeamPoints:function(teamDisplay,changeAmount){
		var shouldUpdate=true;
		if(teamDisplay.teamGameResult.NumPoints+changeAmount<0){
			if(teamDisplay.teamGameResult.NumPoints!=0)teamDisplay.teamGameResult.NumPoints=0;
			else shouldUpdate=false;
		}
		else teamDisplay.teamGameResult.NumPoints+=changeAmount;
		
		//Set the flag that determines which team got to the highest score first(determines winner for tied games)
		if(changeAmount>0&&this.game.GameResult!=null&&this.game.GameResult.TeamGameResults!=null){
			var currentTeam=teamDisplay.teamGameResult.Team;
			var currentTeamPoints=teamDisplay.teamGameResult.NumPoints;
			var hasMostPoints=Object.every(this.game.GameResult.TeamGameResults,
				function(teamGameResult){
					return teamGameResult.Team==currentTeam||currentTeamPoints>teamGameResult.NumPoints;
				});
			if(hasMostPoints)this.firstTeamToTiedValue=currentTeam;
		}
		
		if(shouldUpdate){
			teamDisplay.score.set('text',teamDisplay.teamGameResult.NumPoints);
			this.sendDataToServer();
		}
	},
	changeTeamFouls:function(teamDisplay,changeAmount){
		var shouldUpdate=true;
		if(teamDisplay.teamGameResult.NumFouls+changeAmount<0){
			if(teamDisplay.teamGameResult.NumFouls!=0)teamDisplay.teamGameResult.NumFouls=0;
			else shouldUpdate=false;
		}
		else teamDisplay.teamGameResult.NumFouls+=changeAmount;
		if(shouldUpdate){
			teamDisplay.fouls.set('text',teamDisplay.teamGameResult.NumFouls);
			this.sendDataToServer();
		}
	},
	setGame:function(gameData){
		this.game=gameData;
		this.updateEditDisplay();
	},
	updateEditDisplay:function(){
		if(this.game.GameResult==null)return;
		var gameResult=this.game.GameResult;
		var i=0;
		
		Object.each(gameResult.TeamGameResults,
			function(teamGameResult){
				if(this.editDisplay.teams[i]!=null){
					var teamDisplay=this.editDisplay.teams[i];
					teamDisplay.teamGameResult=teamGameResult;
					this.updateEditDisplayTeam(teamDisplay);
				}
				i++;
			}.bind(this));
		
		//Set the flag that determines which team got to the highest score first(determines winner for tied games)
		var teamWithMostPoints=null;
		var highestPoints=0;
		Object.each(gameResult.TeamGameResults,
			function(teamGameResult){
				if(teamGameResult.NumPoints>highestPoints){
					teamWithMostPoints=teamGameResult.Team;
					highestPoints=teamGameResult.NumPoints;
				}
				else if(teamGameResult.NumPoints==highestPoints)teamWithMostPoints=null;
			}.bind(this));
		this.firstTeamToTiedValue=teamWithMostPoints;
		
		this.showEditDisplay();
		this.show();
	},
	updateEditDisplayTeam:function(teamDisplay){
		if(teamDisplay==null||teamDisplay.teamGameResult==null)return;
		var teamGameResult=teamDisplay.teamGameResult;
		
		var teamString = teamGameResult.TeamName;
		
		if(teamGameResult.TeamName != teamGameResult.Team){
			teamString = teamGameResult.Team + ' - ' + teamGameResult.TeamName;
		}
		
		teamDisplay.teamName.set('text',teamString);
		teamDisplay.score.set('text',teamGameResult.NumPoints);
		teamDisplay.fouls.set('text',teamGameResult.NumFouls);
	},
	sendDataToServer:function(callback){
		if(this.game.GameResult==null)return;
		this.controller.sendGameResultToServer(this.game.GameResult,callback);
	},
	show:function(){
		this.element.show();
		this.fireEvent(this.events.show);
	},
	hide:function(){
		this.element.hide();
		this.fireEvent(this.events.hide);
	},
	showEditDisplay:function(){
		this.hideConfirmDisplay();
		this.editDisplay.show();
		this.currentView=this.views.edit;
		this.fireEvent(this.events.showEditDisplay);
	},
	hideEditDisplay:function(){
		this.editDisplay.hide();
		this.currentView=null;
		this.fireEvent(this.events.hideEditDisplay);
	},
	updateConfirmDisplayTeam:function(teamDisplay){
		if(teamDisplay==null||teamDisplay.teamGameResult==null)return;
		var teamGameResult=teamDisplay.teamGameResult;
		
		var teamString = teamGameResult.TeamName;
		
		if(teamGameResult.TeamName != teamGameResult.Team){
			teamString = teamGameResult.Team + ' - ' + teamGameResult.TeamName;
		}
		
		teamDisplay.teamName.set('text',teamString);
		teamDisplay.score.set('value',teamGameResult.NumPoints);
		teamDisplay.fouls.set('value',teamGameResult.NumFouls);
		if(teamGameResult.WonGame==true)this.setWinnerSelectorValue(teamDisplay.winnerSelector,true,false);
	},
	showConfirmDisplay:function(){
		if(this.game.GameResult==null||this.game.GameResult.TeamGameResults==null)return;
	
		this.resetWinnerSelectors();
		var i=0;
		Object.each(this.game.GameResult.TeamGameResults,
			function(teamGameResult){
				if(this.confirmDisplay.teams[i]!=null){
					var teamDisplay=this.confirmDisplay.teams[i];
					teamDisplay.teamGameResult=teamGameResult;
					this.updateConfirmDisplayTeam(teamDisplay);
				}
				i++;
			}.bind(this));
		
		//Calculate winner
		var teamWithMostPoints;
		var highestPoints=0;
		Object.each(this.game.GameResult.TeamGameResults,
			function(teamGameResult){
				if(teamGameResult.NumPoints>highestPoints){
					teamWithMostPoints=teamGameResult.Team;
					highestPoints=teamGameResult.NumPoints;
				}
				else if(teamGameResult.NumPoints==highestPoints)teamWithMostPoints=null;
			}.bind(this));
		var winner=(teamWithMostPoints!=null?teamWithMostPoints:this.firstTeamToTiedValue);
		if(winner!=null){
			i=0;
			Object.each(this.game.GameResult.TeamGameResults,
				function(teamGameResult,team){
					if(this.confirmDisplay.teams[i]!=null&&team==winner)
						this.setWinnerSelectorValue(this.winnerSelectors[i],true,false);
					else 
						this.setWinnerSelectorValue(this.winnerSelectors[i],false,false);
					i++;
				}.bind(this));
		}
		this.hideEditDisplay();
		this.confirmDisplay.show();
		this.currentView=this.views.confirm;
		this.fireEvent(this.events.showConfirmDisplay);
	},
	hideConfirmDisplay:function(){
		this.confirmDisplay.hide();
		this.currentView=null;
		this.fireEvent(this.events.hideConfirmDisplay);
	},
	confirm:function(){
		if(this.game.GameResult==null||this.confirmDisplay==null||this.confirmDisplay.teams==null)return;
		var winner=null;
		Object.each(this.confirmDisplay.teams,
			function(teamDisplay){
				var teamGameResult=teamDisplay.teamGameResult;
				if(teamDisplay.winnerSelector.selected&&winner==null)winner=teamGameResult.Team;
				teamGameResult.WonGame=teamDisplay.winnerSelector.selected;
				teamGameResult.NumPoints=parseInt(teamDisplay.score.value);
				teamGameResult.NumFouls=parseInt(teamDisplay.fouls.value);
			});
			
		if(winner==null){
			alert('There is currently no winner.\r\nPlease select a winner.');
			return;
		}
		
		this.game.GameResult.WinningTeam=winner;
		this.hideConfirmDisplay();
		this.hide();
		this.sendDataToServer(this.fireEvent.pass(this.events.confirmCompleted,this));
		this.fireEvent(this.events.confirmed);
	},
	handleNumericalTextBoxKeyPress:function(e){
		var charCode = (e.which) ? e.which : ((e.code)?e.code:e.keyCode)
		if (charCode > 31 && (charCode < 48 || charCode > 57))return false;
		return true;
	},
});