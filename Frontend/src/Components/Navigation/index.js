import React from "react";
import { Nav, NavLink, Bars, NavMenu } from "./NavbarElements";
import { Route } from "react-router";
import { useState, useEffect } from "react";
import "../../App.css";
import axios from "axios";
import { BrowserRouter as Router, Switch, Link } from "react-router-dom";
import NavDropdown from "react-bootstrap/NavDropdown";
import jwt_decode from "jwt-decode";

const Logout = () => {
  localStorage.removeItem("jwtToken");
  localStorage.removeItem("username");
};

const AuthButton = (link) => {
  return class extends React.Component {
    render() {

      var role = localStorage.getItem("jwtToken") !== null ? "logat" : "nelogat";
      if (role == "logat" && jwt_decode(localStorage.getItem("jwtToken"))["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]== "admin") {
          role = "admin"
      }

      switch (role) {
        case "admin":
          return (
            <>
              <Router forceRefresh>
                <NavLink to="/usertweets">MyTweets</NavLink>
              </Router>
              <NavDropdown title="Admin Page" id="nav-dropdown">
                <NavDropdown.Item href="/ml-requests">
                  ML Requests
                </NavDropdown.Item>
                <NavDropdown.Item href="/manage-tweets">
                  Manage Tweets
                </NavDropdown.Item>
              </NavDropdown>
              <Router forceRefresh>
                <NavLink to="/" onClick={Logout}>
                  Logout
                </NavLink>
              </Router>
            </>
          );
          case "logat":
          return (
            <>
              <Router forceRefresh>
                <NavLink to="/usertweets">MyTweets</NavLink>
              </Router>
              <Router forceRefresh>
                <NavLink to="/" onClick={Logout}>
                  Logout
                </NavLink>
              </Router>
            </>
          );
        case "nelogat":
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
          
        </Router>

        <NewComponent />
      </NavMenu>
    </Nav>
  );
};

export default Navbar;
