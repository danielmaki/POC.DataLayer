import React, { Component } from 'react';

export class ListFruit extends Component {
  static displayName = ListFruit.name;

  constructor(props) {
    super(props);
    this.state = { fruits: [], loading: true };
  }

  componentDidMount() {
    this.populateTable();
  }

  static renderFruitTable(fruits) {
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
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : ListFruit.renderFruitTable(this.state.fruits);

    return (
      <div>
        <h1 id="tabelLabel" >Fruit List</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateTable() {
    const response = await fetch('fruit');
    const data = await response.json();
    this.setState({ fruits: data, loading: false });
  }
}
