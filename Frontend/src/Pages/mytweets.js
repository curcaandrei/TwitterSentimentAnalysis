import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import ReactPaginate from "react-paginate";
import "../App.css";
import { useHistory } from 'react-router-dom';
import axios from 'axios';




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


const MyTweets = () => {
  var history = useHistory();

//   const [users, setUsers] = useState({hits: []});
//   const [isLoading, setLoading] = useState(true);
//   var [pageNumber, setPageNumber] = useState(0);
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

  console.log(link);
  // https://api.twitter.com/oauth/authorize?oauth_token=dPwLdQAAAAABWPzyAAABfbSZ8u4
  var redirect = link.substring(link.length-27,link.length);
  console.log("REDIRECTUL -> ", redirect);
  
//   useEffect(async () => {
//     const result = await axios({
//         method: 'get',
//         url: 'https://api.twitter.com/oauth/authorize',
//         params: {
//             oauth_token: redirect
//         }
//     });
//     setLoading(false);
// }, []);








if(isLoading){
    return <div className="App4">Loading...</div>;
  }


history.push("/redirect/" + redirect);





  return (
    <Section>
      <Content>
       
        
          <div>
              da
          </div>
        
      </Content>
    </Section>
  );
};

export default MyTweets;
