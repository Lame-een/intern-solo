import React from 'react';
import Display from './Display';

function toTimeInt(milis) {
    let seconds = Math.floor(milis / 1000);
    let minutes = Math.floor(seconds / 60);
    return { min: minutes, sec: (seconds % 60) };
}

function toTimeString(milis) {
    let ret = toTimeInt(milis);
    return { min: ret.min.toString().padStart(2, '0'), sec: ret.sec.toString().padStart(2, '0') };
}

class Timer extends React.Component {
    constructor(props) {
        super(props);

        let duration = (props.min * 60 + props.sec) * 1000;

        this.state = {
            initTime: duration,
            timeLeft: duration,
            goalTime: 0,
            paused: false
        }
        //this.initialize(props.min, props.sec);
        this.initialize = this.initialize.bind(this);
        this.pause = this.pause.bind(this);
        this.unpause = this.unpause.bind(this);
        this.stop = this.stop.bind(this);

    }

    componentDidMount() {
        this.unpause();
        this.timerHandler = setInterval(() => this.interval(), 250);
    }
    componentWillUnmount() {
        clearInterval(this.timerHandler);
    }

    initialize(value) {
        let remaining = toTimeInt(this.state.timeLeft);
        if(value.min){
            var duration = (value.min * 60 + parseInt(remaining.sec)) * 1000;
        }
        else if(value.sec){
            var duration = (remaining.min * 60 + parseInt(value.sec)) * 1000;
        }
        this.setState({ initTime: duration, timeLeft: duration, goalTime: 0, paused: true });
    }

    finished() {
        console.log("finished");
        this.pause();
        //play audio
    }

    interval() {

        if (this.state.paused) return;

        let updateTime = this.state.goalTime - Date.now();

        if (updateTime < 0) {

            this.setState({ timeLeft: 0 });
            this.finished();
        } else {
            this.setState({ timeLeft: updateTime });
        }
    }

    pause() {
        this.setState({ paused: true });
    }

    unpause() {
        let goal = Date.now() + this.state.timeLeft;
        this.setState({ goalTime: goal, paused: false });
    }

    stop() {
        this.setState({ timeLeft: this.state.initTime, paused: true });
    }

    render() {
        return (
            <div>
                <Display time={toTimeString(this.state.timeLeft)} active={true} updateParent={this.initialize} />
                <button onClick={this.state.paused ? this.unpause: this.pause}>{this.state.paused ? 'Unpause': 'Pause'}</button>
                <button onClick={this.stop}>Stop</button>
            </div>
        );
    }
}


export default Timer;