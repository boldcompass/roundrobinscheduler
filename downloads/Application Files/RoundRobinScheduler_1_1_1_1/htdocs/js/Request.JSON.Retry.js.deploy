var GetRetryDelay=
	function(tries){
		if(tries==1)return Number.random(1000,1500);
		else if(tries==2)return Number.random(2000,3000);
		else if(tries<5)return Number.random(5000,6000);
		else if(tries<10)return Number.random(10000,15000);
		else if(tries<15)return Number.random(20000,26000);
		else if(tries<25)return Number.random(30000,38000);
		else return Number.random(40000,50000);
	};
Request.Retry = new Class({
	Extends:Request,
	Binds:'retry',
	tries:0,
	options:{
		getRetryDelay:GetRetryDelay
	},
	initialize:function(options){
		this.addEvent('failure',this.handleError.bind(this),true);
		this.addEvent('error',this.handleError.bind(this),true);
		this.addEvent('success',function(){this.tries==0}.bind(this),true);
		this.parent(options);
	},
	handleError:function(){
		this.tries++;
		this.retry.bind(this).delay(this.options.getRetryDelay(this.tries));
	},
	retry:function(){
		if (!this.running)this.send(this.data)
	},
	send:function(options){
		this.data = options;
		return this.parent(options);
	}
});

Request.JSON.Retry = new Class({
	Extends:Request.JSON,
	options:{
		failOnNull:false,
		getRetryDelay:GetRetryDelay,
		differentFailOnNullDelay:false,
		getFailOnNullRetryDelay:function(tries){return Number.random(5000,6000);}
	},
	initialize:function(options){
		this.addEvent('failure',this.handleError.bind(this),true);
		this.addEvent('error',this.handleError.bind(this),true);
		this.addEvent('success',function(){this.tries==0}.bind(this),true);
		this.parent(options);
	},
	handleError:function(){
		this.tries++;
		this.retry.bind(this).delay(
			(this.isFailOnNull&&this.options.differentFailOnNullDelay)?
			this.options.getFailOnNullRetryDelay(this.tries):
			this.options.getRetryDelay(this.tries));
	},
	retry:function(){
		if (!this.running)this.send(this.data)
	},
	send:function(options){
		this.data = options;
		return this.parent(options);
	},
	success:function(text){
		var json;
		try {
			json = this.response.json = JSON.decode(text, this.options.secure);
		} catch (error){
			this.fireEvent('error', [text, error]);
			return;
		}
		if (json == null && this.options.failOnNull){
			this.isFailOnNull=true;
			this.onFailure();
			this.isFailOnNull=false;
		}
		else this.onSuccess(json, text);
	}
});