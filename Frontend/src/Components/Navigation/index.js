import React from 'react';
import {
  Nav,
  NavLink,
  Bars,
  NavMenu} from './NavbarElements';
import { Route } from 'react-router';
import { useState, useEffect } from 'react';
import "../../App.css";
import axios from 'axios';


const Navbar = () => {
  const [link, setLink] = useState("");
  // const [tweets, setTweets] = useState({hits: []});
  const [isLoading, setLoading] = useState(true);
    
  useEffect(() => {
      axios({
          method: 'post',
          url: 'https://localhost:7225/signin'
      }).then(function (res) {
        setLink(res.data);
        setLoading(false);
      });
  }, []);

  if(isLoading){
    return (
      <>
        <Nav>
          <NavLink to='/'>
            Tweet Sentiment Analyzer
          </NavLink>
          <Bars />
          <NavMenu>
            <NavLink to='/about' >
              About
            </NavLink>
            <NavLink to='/mytweets' >
              My Tweets
            </NavLink>
            
          </NavMenu>
        </Nav>
      </>
    );
  }
  
  return (
    <>
      <Nav>
        <NavLink to='/'>
          Tweet Sentiment Analyzer
        </NavLink>
        <Bars />
        <NavMenu>
          <NavLink to='/about' >
            About
          </NavLink>
          <NavLink to='/mytweets' >
            My Tweets
          </NavLink>
          <Route path='/mytweets' component={() => { 
    window.location.href = link;
    return null;
}}/>
          
        </NavMenu>
      </Nav>
    </>
  );
};

export default Navbar;