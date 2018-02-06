var _3o3={};

//
//Begin Controller
//
_3o3.getController = function(){
	if (_3o3.controllerObject == null){
		_3o3.controllerObject = new _3o3.Controller();
	}
	return _3o3.controllerObject;
};
_3o3.Controller = new Class({
	Implements: [Events,Options],
	events:{
		gameChanged:'gameChanged',
		accessCodeChanged:'accessCodeChanged',
		stringsChanged:'stringsChanged',
		scoreKeeperDataChanged:'scoreKeeperDataChanged',
		scoreKeeperScheduleChanged:'scoreKeeperScheduleChanged'
	},
	options:{},
	initialize: function(options){
		this.setOptions(options);
		
		this.getData({action:'strings',callback:this.setStrings.bind(this)});
		this.getData({action:'accesscode',callback:this.setAccessCode.bind(this)});
	},
	setAccessCode: function(accessCode){
		this.accessCode = accessCode;
		this.fireEvent(this.events.accessCodeChanged,accessCode);
		this.refreshScoreKeeperData();
	},
	refreshScoreKeeperData:function(maxDelay){
		if(!this.accessCode){
			this.refreshScoreKeeperData.bind(this).delay(Number.random(5000,6000));
			return;
		}
		this.getData({action:'currentscorekeeper',failOnNull:false,delay:maxDelay,callback:this.setScoreKeeperData.bind(this)});
	},
	setScoreKeeperData: function(scoreKeeperData){
		if(scoreKeeperData!=null)this.refreshScoreKeeperData(30000);
		else this.refreshScoreKeeperData(Number.random(5000,6000));
		if(scoreKeeperData!=null && (this.scoreKeeperData==null||this.scoreKeeperData.Id!=scoreKeeperData.Id))this.getCurrentGame();
		this.scoreKeeperData=scoreKeeperData;
		this.fireEvent(this.events.scoreKeeperDataChanged,scoreKeeperData);
	},
	setStrings: function(strings){
		this.strings = strings;
		this.fireEvent(this.events.stringsChanged,strings);
	},
	sendGameResultToServer:function(gameData,callback){
		if(gameData==null||gameData.GameId==null)return;
		
		this.getData({action:'setgameresult',data:JSON.encode(gameData),callback:callback,failOnNull:false,json:false,forcePost:true});
	},
	setGame:function(gameId){
		if(gameId==null)this.gameData=null;
		else this.getGame(gameId);
	},
	getCurrentGame: function(maxDelay){
		this.getData({action:'currentgame',delay:maxDelay,callback:this.gameDataReturned.bind(this)});
	},
	getGame: function(id,maxDelay){
		this.getData({action:'game',data:id,delay:maxDelay,callback:this.gameDataReturned.bind(this)});
	},
	getSchedule: function(callback,maxDelay){
		this.getData({action:'scorekeeperschedule',delay:maxDelay,callback:
			function(data){
				this.scheduleDataReturned(data,callback);
			}.bind(this)});
	},
	scheduleDataReturned:function(scheduleData,callback){
		this.fireEvent(this.events.scoreKeeperScheduleChanged,scheduleData);
		if(typeof(callback)=='function')callback(scheduleData);
		
		this.getSchedule(null,Number.random(30000,45000));
	},
	gameDataReturned: function(gameData){
		//Reload data later if needed
		var refreshDelay;
		if(!this.getGamesAreEqual(gameData,this.gameData)){
			
			if(
				this.gameData==null||gameData==null||
				((this.gameData.NotAssigned==null||gameData.NotAssigned==null||this.gameData.NotAssigned==gameData.NotAssigned)&&
				(this.gameData.GameCompleted==null||gameData.GameCompleted==null||this.gameData.GameCompleted==gameData.GameCompleted)))
			{
				this.gameData=gameData;	
				
				this.fireEvent(this.events.gameChanged,gameData);
			}
		}
		if(gameData!=null){
			if(gameData.NotAssigned)refreshDelay=Number.random(15000,20000);
			else if(gameData.GameCompleted)refreshDelay=Number.random(5000,7500);
		}
		if(refreshDelay!=null)this.getCurrentGameDelay=this.getCurrentGame(refreshDelay);
	},
	getGamesAreEqual:function(game1,game2){
		if(game1==game2)return true;
		if(game1==null||game2==null)return false;
		if(game1.Id!=game2.Id)return false;
		if(game1.RobinRoundNum!=game2.RobinRoundNum)return false;
		if(game1.CourtRoundNum!=game2.CourtRoundNum)return false;
		if(game1.CourtNum!=game2.CourtNum)return false;
		if(game1.Team1!=game2.Team1)return false;
		if(game1.Team2!=game2.Team2)return false;
		if(game1.ScoreKeeper!=game2.ScoreKeeper)return false;
		
		return true;
	},
	getTournamentStatus:function(callback){
		this.getData({action:'tournamentstatus',callback:callback});
		this.getData({action:'tournamentstatus',callback:callback});
	},
	nextCallTime:null,
	nextCallTimer:null,
	callsParams:{},
	callsSortIndex:-1,
	getData:function(options){
		if(options.action==null)return;
		
		this.callsSortIndex++;
		options.sortIndex=this.callsSortIndex;
		
		this.callsParams[options.action]=options;
		
		if(typeof(options.delay)!='number'||options.delay<=0)this.sendCalls();
		else this.sendCallsWithin(options.delay);
	},
	sendCallsWithin:function(maxDelay){
		var now=new Date().getTime();
		var sendBy=now+maxDelay;
		if(this.nextCallTime==null||this.nextCallTime<now||sendBy<this.nextCallTime){
			if(this.nextCallTimer!=null)clearTimeout(this.nextCallTimer);
			this.nextCallTimer=this.sendCalls.delay(maxDelay,this);
			this.nextCallTime=sendBy;
		}
	},
	sendCalls:function(){
		var numItems=Object.getLength(this.callsParams);
		if(numItems<=0)return
		var firstItem=Object.values(this.callsParams)[0];
		
		var method='POST';
		if(numItems<=1){
			if(firstItem.data==null||firstItem.data.toString().length<16&&firstItem.forcePost!=true)method='GET';
		}
		
		var oldCallsParams=this.callsParams;
		
		var requestType=Request.JSON;
		var url='json/'
		var data={};
		if(numItems>1){
			this.sortBySortIndex(this.callsParams).each(
				function(pair){
					var callData=pair.value;
					if(callData.data==null)data[callData.action]="";
					else data[callData.action]=typeof(callData.data)=='string'?callData.data:JSON.encode(callData.data);
				});
		}
		else{
			oldCallsParams.isSingleCall=true;
			
			data={d:typeof(firstItem.data)=='string'?firstItem.data:JSON.encode(firstItem.data)};
			requestType=(firstItem.json==false?Request:Request.JSON);
			url+=firstItem.action;
		}

		var requestOptions={method:method,data:data};
		var request=new requestType(
		{
			noCache:true,
			url:url,
			onFailure:this.handleCallError.pass(oldCallsParams,this),
			onSuccess:
				function(data){
					var ret=this.handleCallsReturned(oldCallsParams,data);
					if(ret!=null)this.handleCallError(ret);
				}.bind(this)
		});
		request.send(requestOptions);
	
		this.callsParams={};
		if(this.nextCallTimer!=null){
			clearTimeout(this.nextCallTimer);
			this.nextCallTime==null;
		}
	},
	sortBySortIndex:function(object){
		var sortedKeys=Object.keys(object).sort(
				function(a,b){
					return object[a].sortIndex - object[b].sortIndex;
				});
		var sorted=[];
			sortedKeys.each(
				function(key){
					sorted.push({key:key,value:object[key]});
				});
		return sorted;
	},
	handleCallsReturned:function(callsParams,data){
		var failedCalls={};
		if(!callsParams.isSingleCall){
			if(data==null)return callsParams
						
			this.sortBySortIndex(callsParams).each(
				function(pair){
					var callParams=pair.value;
					var action=pair.key;
					
					var hadError=false;
					if(typeof(data[action])=='undefined')hadError=true;
					else{
						var actionData=data[action];
						if(this.handleCallReturned(callParams,action,actionData)==false)hadError=true;
					}
					if(hadError)
						failedCalls[action]=callParams
				}.bind(this));			
		}
		else{
			var callParams=Object.values(callsParams)[0];
			if(this.handleCallReturned(callParams,callParams.action,data)==false)
				failedCalls[callParams.action]=callParams;
		}
		if(Object.getLength(failedCalls)>0)
			return(failedCalls);
	},
	handleCallReturned:function(callParams,action,actionData){
		if((actionData==null&&callParams.failOnNull!=false)||(actionData!=null&&actionData.Error))
			return false;
		else if(typeof(callParams.callback)=='function')callParams.callback(actionData);
	},
	handleCallError:function(callsParams){
		var minRetries=1;
		delete callsParams.isSingleCall;
		Object.each(callsParams,
			function(callParam,action){
				if(callParam.retries==null)callParam.retries=1;
				else{
					callParam.retries++;
					if(callParam.retries>minRetries)minRetries=callParam.retries
				}
			});
		this.callsParams=Object.append(callsParams,this.callsParams);
		var maxDelay=GetRetryDelay(minRetries);
		this.sendCallsWithin(maxDelay);
	}
});

//http://stackoverflow.com/questions/1038746/equivalent-of-string-format-in-jquery/5095933#5095933
function getStringFormatPlaceHolderRegEx(placeHolderIndex) {
    return new RegExp('({)?\\{' + placeHolderIndex + '\\}(?!})', 'gm')
}

function cleanStringFormatResult(txt) {
    if (txt == null) return "";

    return txt.replace(getStringFormatPlaceHolderRegEx("\\d+"), "");
}

String.prototype.format = function () {
    var txt = this.toString();
    for (var i = 0; i < arguments.length; i++) {
        var exp = getStringFormatPlaceHolderRegEx(i);
        txt = txt.replace(exp, (arguments[i] == null ? "" : arguments[i]));
    }
    return cleanStringFormatResult(txt);
}
String.format = function () {
    var s = arguments[0];
    if (s == null) return "";

    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = getStringFormatPlaceHolderRegEx(i);
        s = s.replace(reg, (arguments[i + 1] == null ? "" : arguments[i + 1]));
    }
    return cleanStringFormatResult(s);
}