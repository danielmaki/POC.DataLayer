import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { ListFruits } from './components/ListFruits';
import { GetFruit } from './components/GetFruit';
import { AddFruit } from './components/AddFruit';
import { ChangeFruit } from './components/ChangeFruit';
import { RemoveFruit } from './components/RemoveFruit';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/list-fruits' component={ListFruits} />
        <Route path='/get-fruit' component={GetFruit} />
        <Route path='/add-fruit' component={AddFruit} />
        <Route path='/change-fruit' component={ChangeFruit} />
        <Route path='/remove-fruit' component={RemoveFruit} />
      </Layout>
    );
  }
}
