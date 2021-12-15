import React, { useState, useEffect } from 'react';
import { PieChart, Pie, ResponsiveContainer } from "recharts";
import { useLocation } from "react-router";
import axios from 'axios';

const AnalyzerDB = () => {
  
  let location = useLocation();
  let url = location.pathname;
  let tweet_id = url.substring(12, url.length);
  var data2 = [];
  
  const [tweet, setTweet] = useState("");
  const [happy, setHappy] = useState("");
  const [sad, setSad] = useState("");
  const [data, setData] = useState([]);

  useEffect(() => {
    axios("https://localhost:7225/api/Tweets/one/" + tweet_id,{  
    }).then( function (res) {
      setTweet(res.data);
      for(const prop in res.data.feels){
        if(prop === "sad"){
          setSad(res.data.feels[prop]);
        }
        else if(prop === "happy"){
          setHappy(res.data.feels[prop]);
        }
      }
      setData([
        { name: "Happy", value: Number(parseFloat(happy*100).toFixed(2))  },
        { name: "Sad", value: Number(parseFloat(sad*100).toFixed(2)) },
      ]);
    })
    
  }, []);
  
  
  
  for(var prop in data){
    if(data[prop].name === "Happy")
      data2.push({ name: "Happy", value: Number(parseFloat(happy*100).toFixed(2)), fill: '#bbe7d5' })
    else if(data[prop].name === "Sad")
      data2.push( { name: "Sad", value: Number(parseFloat(sad*100).toFixed(2)), fill: '#d8bfbb' })
  }
  
  return (
    <div>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          "marginTop": "4rem",
          "fontSize": "28px",
        }}
      >
        <h1>Statistics about this tweet</h1>
      </div>

      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "68vh",
          "flexDirection": "column",
          "marginBottom": "5rem",
        }}
      >
        <div className="block-parent-analyzer">
          <div className="tweet-list">
            <div className="tweet">
              <div className="twitter-icon">
                <div className="Icon Icon--twitter"></div>
              </div>
              <div className="tweet-author">
                <div className="TweetAuthor">
                  <a className="TweetAuthor-link" href="#channel">
                    {" "}
                  </a>
                  <span className="TweetAuthor-avatar">
                    <div className="Avatar"> </div>
                  </span>
                  <span className="TweetAuthor-name">{tweet.user}</span>{" "}
                  <span className="TweetAuthor-screenName">@{tweet.user}</span>
                </div>
              </div>
              <div className="tweet-text">{tweet.text}</div>
              <div className="tweet-timestamp">
                <span className="tweet-timestamp-date">
                  {tweet.date}
                </span>
              </div>
            </div>
          </div>
        </div>

        <ResponsiveContainer width="30%" height={300}>
          <PieChart height={250}>
            <Pie
              data={data2}
              cx="50%"
              cy="50%"
              outerRadius={100}
              fill="green"
              dataKey="value"
              label={({
                cx,
                cy,
                midAngle,
                innerRadius,
                outerRadius,
                value,
                index,
              }) => {
                console.log("handling label?");
                const RADIAN = Math.PI / 180;
                const radius = 25 + innerRadius + (outerRadius - innerRadius);
                const x = cx + radius * Math.cos(-midAngle * RADIAN);
                const y = cy + radius * Math.sin(-midAngle * RADIAN);

                return (
                  <text
                    x={x}
                    y={y}
                    fill="black"
                    textAnchor={x > cx ? "start" : "end"}
                    dominantBaseline="central"
                  >
                    {data2[index].name} ({value}%)
                  </text>
                );
              }}
            />
          </PieChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
};

export default AnalyzerDB;
