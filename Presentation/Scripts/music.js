//audio
var mytrack=document.getElementById('myTrack');
var playButton = document.getElementById('playButton');
var muteButton = document.getElementById('muteButton');

var duration = document.getElementById('fullDuration');
var currentTime = document.getElementById('currentTime');

var minutes = pad(parseInt(mytrack.duration/60));
var seconds = pad(parseInt(mytrack.duration%60));

var bar = document.getElementById('defultBar');
var barSize = bar.style.width=500;

var progressBar = document.getElementById('progressBar');

mytrack.addEventListener("loadedmetadata", function () {
    var minutes = pad(parseInt(mytrack.duration / 60));
    var seconds = pad(parseInt(mytrack.duration % 60));
    duration.innerHTML = minutes + ':' + seconds;
});

playButton.addEventListener('click',playOrPause, false);
muteButton.addEventListener('click',muteOrUnmute, false);

function playOrPause(){
	if(!mytrack.paused && !mytrack.ended){
		mytrack.pause();
        
        playButton.classList.remove('pause');
		
		window.clearInterval(updateTime);//stop when pause
	}
	else{
		mytrack.play();
        
        playButton.classList.add('pause');
		
		updateTime = setInterval(update,500); //update time each
	}
}

function muteOrUnmute(){
	if(mytrack.muted == true){
		mytrack.muted = false;
		
       muteButton.classList.remove('mute');
        
	}
	else{
		mytrack.muted = true;
		
        muteButton.classList.add('mute');
        
	}
}

function update(){
	if(!mytrack.ended){
		var playedMinutes = pad(parseInt(mytrack.currentTime/60));
		var playedSeconds = pad(parseInt(mytrack.currentTime%60));
		currentTime.innerHTML = playedMinutes + ':' + playedSeconds;
		
		
		var size = parseInt(mytrack.currentTime*barSize/mytrack.duration);
		progressBar.style.width = size + "px";
	}
	else{
		currentTime.innerHTML = "0.00";
		
		playButton.classList.remove('pause');
		
		progressBar.style.width = "0px";
		window.clearInterval(updateTime);
	}
}

function pad(d){
	return (d<10) ? '0'+d.toString() : d.toString();
}
