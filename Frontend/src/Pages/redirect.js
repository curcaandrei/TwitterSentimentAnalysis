import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import ReactPaginate from "react-paginate";
import "../App.css";
import { useHistory } from 'react-router-dom';
import axios from 'axios';
import { useLocation } from "react-router";




const Section = styled.section`
  background-image: url(${BgImg});
  
  height: 785px;
  display: block;
  background-repeat: no-repeat;
  background-size: contain;
`;

const Content = styled.div`
  width: 100%;
  height: 100px;
`;


const Redirect = () => {
  var history = useHistory();

  let location = useLocation();
  let url = location.pathname;
    let oauth_token = url.substring(10, url.length);
    console.log(oauth_token);

    const [text, setText] = useState({hits: []});
  
  useEffect(async() => {
    const result = axios({
        method: 'get',
        url: 'https://api.twitter.com/oauth/authorize',
        params: {
            oauth_token: oauth_token
        }
    });
    setText(result.data);

}, []);









// if(isLoading){
//     return <div className="App4">Loading...</div>;
//   }


// history.push("/redirect/" + redirect);





  return (
    <Section>
      <Content>
       
        
      <div dangerouslySetInnerHTML={{ __html: text}}></div>
        
      </Content>
    </Section>
  );
};

export default Redirect;
