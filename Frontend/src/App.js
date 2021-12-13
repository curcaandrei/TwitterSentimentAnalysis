import './App.css';

import React from 'react';
import Navbar from './Components/Navigation';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Home from './Pages';
import About from './Pages/about';
import Tweets from './Pages/tweets';
import Contact from './Pages/contact';
import Homee from './Pages/homee';
import Analyzer from './Pages/analyzer';
import AnalyzerDB from './Pages/analyzerdb';
import MyTweets from './Pages/mytweets';
import Redirect from './Pages/redirect';


function App() {

  return (
    
    <Router>  
      <Navbar/>
     {/* // <Homee/> */}
      <Switch>
        <Route path='/' exact component={Homee} />
        <Route path='/about' component={About} />
        <Route path='/tweets' component={Tweets} />
        <Route path='/contact-us' component={Homee} />
        <Route path='/analyzer' component={Analyzer} />
        <Route path='/analyzerdb' component={AnalyzerDB} />
        <Route path='/mytweets' component={MyTweets} />
        <Route path='/redirect' component={Redirect} />
      </Switch>
    </Router>
     
       
  );
}

export default App;
