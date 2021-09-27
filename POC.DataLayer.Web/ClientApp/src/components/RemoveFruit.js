import React, { Component } from 'react';

export class RemoveFruit extends Component {
    constructor(props) {
        super(props);

        this.state = { Id: 0 };
        this.HandleIdChange = this.HandleIdChange.bind(this);
        this.HandleSubmit = this.HandleSubmit.bind(this);
    }

    HandleIdChange(event) {
        this.setState({ Id: event.target.value });
    }

    HandleSubmit(event) {
        event.preventDefault();

        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        };

        fetch('fruit/Delete/' + this.state.Id, requestOptions)
            .then(response => console.log('Submitted successfully', response))
            .catch(error => console.log('Form submit error', error))
    }

    render() {
        return (
            <form onSubmit={this.HandleSubmit}>
                <label>Id: <input type="number" value={this.state.Id} onChange={this.HandleIdChange} /></label>
                <br />
                <input type="submit" value="Submit" />
            </form>
        );
    }
}
