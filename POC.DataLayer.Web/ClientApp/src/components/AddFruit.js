import React, { Component } from 'react';

export class AddFruit extends Component {
    constructor(props) {
        super(props);

        this.state = { Id: 0, Name: "Unknown", Taste: "Unknown", Color: "Unknown" };
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleTasteChange = this.handleTasteChange.bind(this);
        this.handleColorChange = this.handleColorChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleNameChange(event) {
        this.setState({ Name: event.target.value });
    }

    handleTasteChange(event) {
        this.setState({ Taste: event.target.value });
    }

    handleColorChange(event) {
        this.setState({ Color: event.target.value });
    }

    handleSubmit(event) {
        event.preventDefault();

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.state)
        };

        fetch('fruit', requestOptions)
            .then(response => console.log('Submitted successfully', response))
            .catch(error => console.log('Form submit error', error))
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>Name: <input type="text" value={this.state.Name} onChange={this.handleNameChange} /></label><br />
                <label>Create Fruit: <select value={this.state.Taste} onChange={this.handleTasteChange}>
                        <option value="Unknown">Unknown</option>
                        <option value="Sweet">Sweet</option>
                        <option value="Sour">Sour</option>
                    </select></label><br />
                <label>Color: <input type="text" value={this.state.Color} onChange={this.handleColorChange} /></label><br />
                <input type="submit" value="Submit" />
            </form>
        );
    }
}
