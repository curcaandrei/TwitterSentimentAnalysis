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
  const [tweets, setTweets] = useState({hits: []});
  const [isLoading, setLoading] = useState(true);
    
  useEffect(async () => {
      const result = await axios({
          method: 'post',
          url: 'https://localhost:7225/signin'
      });
      setLink(result.data);
      setLoading(false);
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
            <NavLink to='/about' activeStyle>
              About
            </NavLink>
            <NavLink to='/mytweets' activeStyle>
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
          <NavLink to='/about' activeStyle>
            About
          </NavLink>
          <NavLink to='/mytweets' activeStyle>
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