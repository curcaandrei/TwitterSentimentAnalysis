import './App.css';

import React from 'react';
import Navbar from './Components/Navigation';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Homee from './Pages/homee';
import Analyzer from './Pages/analyzer';
import AnalyzerDB from './Pages/analyzerdb';
import MyTweets2 from './Pages/signin';
import MyTweets3 from './Pages/mytweets';
import MLRequests from './Pages/MLRequests';
import ManageTweets from './Pages/manage-tweets';
import Admin from './Pages/admin';

function App() {
  return (
    
    <Router>  
      <Navbar/>
     {/* // <Homee/> */}
      <Switch>
        <Route path='/' exact component={Homee} />
        <Route path='/contact-us' component={Homee} />
        <Route path='/analyzer' component={Analyzer} />
        <Route path='/analyzerdb' component={AnalyzerDB} />
        <Route path='/signin' component={MyTweets2} />
        <Route path='/usertweets' component={MyTweets3} />
        <Route path='/ml-requests' component={MLRequests}/>
        <Route path='/editTweets' component={Admin}/>
        <Route path='/manage-tweets' component={ManageTweets}/>
      </Switch>
    </Router>
     
       
  );
}

export default App;
