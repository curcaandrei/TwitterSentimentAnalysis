import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import ReactPaginate from "react-paginate";
import "./admin.css";
import { useHistory } from 'react-router-dom';
import axios from 'axios';
import Sidebar from '../Components/Navigation/Sidebar';
import { Review } from './reviewTweets';
import Edit  from './editTweets';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import './../../node_modules/bootstrap/dist/css/bootstrap.min.css';

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


const Admin = () => {


  // var [pageNumber, setPageNumber] = useState(0);
  // var [loading, setLoading] = useState(true);

  // const [users, setUsers] = useState("");
  // // const [isLoading, setLoading] = useState(true);
  // pageNumber++;


  // useEffect(() => {
  //   axios("https://localhost:7225/api/Tweets/all/" + pageNumber,{
  //   }).then(res => setUsers(res.data));
  //   setLoading(false);
  // }, [pageNumber]);


  // // const usersPerPage = 10;
  // // const pagesVisited = pageNumber * usersPerPage;
  
  // var url = "";
  // var tweet_id = "";
  // const history = useHistory();

   
  
  // const displayUsers = Object.keys(users).map((user,i) => {
    
  //     return (
  //       <div className="block-parent" key={i}>
  //         <div className="tweet-list" key={++i}>
  //           <div className="tweet" key={++i}>
  //             <div className="twitter-icon" key={++i}>
  //               <div className="Icon Icon--twitter" key={++i}></div>
  //             </div>
  //             <div className="tweet-author" key={++i}>
  //               <div className="TweetAuthor" key={++i}><a className="TweetAuthor-link" href="#channel"> </a><span className="TweetAuthor-avatar"> 
  //                 <div className="Avatar" key={++i}> </div></span><span className="TweetAuthor-name">{users[user].user}</span> <span className="TweetAuthor-screenName">@{users[user].user}</span></div>
  //               </div>
  //               <div className="tweet-text" key={++i}>{users[user].text}</div>
  //               <div className="tweet-timestamp" key={++i}>
  //                 <span className="tweet-timestamp-date" >{users[user].date}</span>
  //               </div>
  //             </div>
  //           </div>
  //           {/* <Link to={href} classNameName="button">Analyze</Link> */}
  //           <a href={"/analyzerdb/" + users[user].id} className="button">Analyze</a>
  //         </div>
  //     );
  //   });

  // const pageCount = 104800;

  // const changePage = ({ selected }) => {
  //   setPageNumber(selected);
  // };
  return (
    <Section>
      <Content>
         
    <React.Fragment>
      <Router>
        <Sidebar />
        <Switch>
        <Route path='/reviewTweets' component={Review} />
        <Route path='/editTweets' component={Edit}/>
        </Switch>
      </Router>
    </React.Fragment>

       {/* <div className="App">
      {displayUsers}
      <ReactPaginate
        previousLabel={"Previous"}
        nextLabel={"Next"}
        pageCount={pageCount}
        onPageChange={changePage}
        containerClassName={"paginationBttns"}
        previousLinkClassName={"previousBttn"}
        nextLinkClassName={"nextBttn"}
        disabledClassName={"paginationDisabled"}
        activeClassName={"paginationActive"}
      />
    </div> */}
        
      </Content>
    </Section>
  );
};

export default Admin;
