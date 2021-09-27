import React, { Component } from 'react';

export class GetFruit extends Component {
  static displayName = GetFruit.name;

  constructor(props) {
    super(props);
      this.state = {
          Id: 0,
          Fruit: { Name: "", Taste: "", Color: "" },
          Loading: false
      };
      this.HandleIdChange = this.HandleIdChange.bind(this);
      this.HandleSubmit = this.HandleSubmit.bind(this);
    }

    HandleIdChange(event) {
        this.setState({ Id: event.target.value });
    }

    HandleSubmit(event) {
        event.preventDefault();

        this.setState({ Loading: true })

        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        };

        fetch('fruit/Get/' + this.state.Id, requestOptions)
            .then(response => {
                response.json().then(data => { this.setState({ Fruit: data, Loading: false }) });
            })
            .catch(error => {
                console.log('Form submit error', error);
                this.setState({ Fruit: { Id: 0, Name: "", Taste: "", Color: "" }, Loading: false });
            });
    }

  static RenderFruit(fruit) {
    return (
      <div>
        <p>Name: {fruit.name}</p>
        <p>Taste: {fruit.taste}</p>
        <p>Color: {fruit.color}</p>
      </div>
    );
  }

  render() {
    let contents = this.state.Loading
      ? <p><em>Loading...</em></p>
        : GetFruit.RenderFruit(this.state.Fruit);

    return (
        <div>
            <h1 id="tabelLabel" >Fruit List</h1>
            <form onSubmit={this.HandleSubmit}>
                <label>Id: <input type="number" value={this.state.Id} onChange={this.HandleIdChange} /></label>
                <br />
                <input type="submit" value="Submit" />
            </form>
            <br />
            {contents}
        </div>
    );
  }
}
