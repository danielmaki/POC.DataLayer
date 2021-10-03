import React, { Component } from 'react';

export class AddFruit extends Component {
    constructor(props) {
        super(props);

        this.state = { Id: 0, Name: "Unknown", Taste: "Unknown", Color: "Unknown" };
        this.HandleNameChange = this.HandleNameChange.bind(this);
        this.HandleTasteChange = this.HandleTasteChange.bind(this);
        this.HandleColorChange = this.HandleColorChange.bind(this);
        this.HandleSubmit = this.HandleSubmit.bind(this);
    }

    HandleNameChange(event) {
        this.setState({ Name: event.target.value });
    }

    HandleTasteChange(event) {
        this.setState({ Taste: event.target.value });
    }

    HandleColorChange(event) {
        this.setState({ Color: event.target.value });
    }

    HandleSubmit(event) {
        event.preventDefault();

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.state)
        };

        fetch('fruit/Create', requestOptions)
            .then(response => console.log('Submitted successfully', response))
            .catch(error => console.log('Form submit error', error))
    }

    render() {
        return (
            <form onSubmit={this.HandleSubmit}>
                <label>Name: <input type="text" value={this.state.Name} onChange={this.HandleNameChange} /></label>
                <br />
                <label>Taste: <select value={this.state.Taste} onChange={this.HandleTasteChange}>
                        <option value="Unknown">Unknown</option>
                        <option value="Sweet">Sweet</option>
                        <option value="Sour">Sour</option>
                        <option value="Bitter">Bitter</option>
                        <option value="No Taste">No Taste</option>
                    </select>
                </label>
                <br />
                <label>Color: <input type="text" value={this.state.Color} onChange={this.HandleColorChange} /></label>
                <br />
                <input type="submit" value="Submit" />
            </form>
        );
    }
}
