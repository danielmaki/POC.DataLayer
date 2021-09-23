import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { ListFruit } from './components/ListFruit';
import { AddFruit } from './components/AddFruit';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/add-fruit' component={AddFruit} />
        <Route path='/list-fruits' component={ListFruit} />
      </Layout>
    );
  }
}
