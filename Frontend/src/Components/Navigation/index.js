import React from "react";
import { Nav, NavLink, Bars, NavMenu } from "./NavbarElements";
import { Route } from "react-router";
import { useState, useEffect } from "react";
import "../../App.css";
import axios from "axios";
import { BrowserRouter as Router, Switch, Link } from "react-router-dom";

const Logout = () => {
  localStorage.removeItem("jwtToken");
  localStorage.removeItem("username");
};

const AuthButton = (link) => {
  return class extends React.Component {
    render() {
      var isLoggedIn = localStorage.getItem("jwtToken") !== null ? true : false;

      switch (isLoggedIn) {
        case true:
          return (
            <>
              <Router forceRefresh>
                <NavLink to="/usertweets">MyTweets</NavLink>
              </Router>
              <Router forceRefresh>
                <NavLink to="/editTweets">Admin</NavLink>
              </Router>
              <Router forceRefresh>
                <NavLink to="/" onClick={Logout}>
                  Logout
                </NavLink>
              </Router>
            </>
          );
        case false:
          return (
            <>
              <NavLink to="/mytweets">Sign In</NavLink>
              <Route
                path="/mytweets"
                component={() => {
                  window.location.href = link;
                  return null;
                }}
              />
            </>
          );
        default:
          return null;
      }
    }
  };
};

const Navbar = () => {
  const [link, setLink] = useState("");
  useEffect(() => {
    axios({
      method: "post",
      url: "https://localhost:7225/signin",
    }).then(function (res) {
      setLink(res.data);
      // setLoading(false);
    });
  }, []);

  const NewComponent = AuthButton(link);

  return (
    <Nav>
      <Router forceRefresh>
        <NavLink to="/">Tweet Sentiment Analyzer</NavLink>
      </Router>

      <Bars />

      <NavMenu>
        <Router forceRefresh>
          <NavLink to="/about">About</NavLink>
        </Router>

        <NewComponent />
      </NavMenu>
    </Nav>
  );
};

export default Navbar;
