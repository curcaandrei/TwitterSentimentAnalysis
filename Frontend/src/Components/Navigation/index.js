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

const AuthButton = (link) => {
  return class extends React.Component {
    render() {
      var isLoggedIn = localStorage.getItem('jwtToken') != null ? true : false;

      switch(isLoggedIn) {
          case true:
              return (
                  <NavLink to='/usertweets' >
                          MyTweets
                        </NavLink>
                  )
          case false:
              return (
                  <><NavLink to='/mytweets'>
                  Sign In
                </NavLink><Route path='/mytweets' component={() => {
                  window.location.href = link;
                  return null;
                } } /></>
                )
          default:
              return null;
      }
    }
  }
  
}



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

  const NewComponent = AuthButton(link);

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
            <NewComponent />
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

          <NewComponent />
            

          
        
        </NavMenu>
      </Nav>
    </>
  );
};

export default Navbar;