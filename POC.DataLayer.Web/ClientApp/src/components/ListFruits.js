import React, { Component } from 'react';

export class ListFruits extends Component {
  static displayName = ListFruits.name;

  constructor(props) {
    super(props);
    this.state = { Fruits: [], Loading: true };
  }

  componentDidMount() {
    this.PopulateTable();
  }

  static RenderFruitTable(fruits) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Color</th>
            <th>Taste</th>
          </tr>
        </thead>
        <tbody>
          {fruits.map(fruit =>
            <tr key={fruit.id}>
              <td>{fruit.id}</td>
              <td>{fruit.name}</td>
              <td>{fruit.color}</td>
              <td>{fruit.taste}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.Loading
      ? <p><em>Loading...</em></p>
      : ListFruits.RenderFruitTable(this.state.Fruits);

    return (
      <div>
        <h1 id="tabelLabel" >Fruit List</h1>
        {contents}
      </div>
    );
  }

  async PopulateTable() {
    const response = await fetch('fruit/GetAll');
    const data = await response.json();
    this.setState({ Fruits: data, Loading: false });
  }
}
