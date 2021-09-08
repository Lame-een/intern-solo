import React from 'react';

class Display extends React.Component {
    constructor(props) {
        super(props);
        this.state = { min: this.props.time.min, sec: this.props.time.sec, minFocus: false, secFocus: false };
        console.log(this.props.time.min, this.props.time.sec);
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        let target = event.target;
        this.setState({ [target.name]: target.value });
    }

    handleSubmit(event) {
        let target = event.target;
        if (target.name === "min") {
            this.props.updateParent({ min: this.state.min });
        } else if (target.name === "sec") {
            this.props.updateParent({ sec: this.state.sec });
        }
    }

    render() {
        return (
            <div>
                <input name="min"
                    type="text"
                    value={this.state.minFocus ? this.state.min : this.props.time.min}
                    onChange={this.handleChange}
                    onFocus={() => { this.setState({ minFocus: true, min: this.props.time.min }) }}
                    onBlur={(event) => { this.setState({ minFocus: false }); return this.handleSubmit(event); }}
                />

                {this.props.active ? ':' : ' '}

                <input name="sec"
                    value={this.state.secFocus ? this.state.sec : this.props.time.sec}
                    onChange={this.handleChange}
                    onFocus={() => { this.setState({ secFocus: true, sec: this.props.time.sec }) }}
                    onBlur={(event) => { this.setState({ secFocus: false }); return this.handleSubmit(event); }}
                />
            </div>
        );
    }
}


export default Display;