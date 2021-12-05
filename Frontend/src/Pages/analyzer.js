import React from "react";
import Navbar from "../Components/Navigation";
import { PieChart, Pie, ResponsiveContainer } from "recharts";

const Analyzer = () => {
  const data = [
    { name: "Happy", value: 50 },
    { name: "Sad", value: 30 },
    { name: "Neutral", value: 20 },
  ];
  return (
    <div>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          "margin-top": "4rem",
          "font-size": "28px"
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
          "flex-direction": "column",
          "margin-bottom": "5rem"
        }}
      >
        <div class="block-parent-analyzer">
          <div class="tweet-list">
            <div class="tweet">
              <div class="twitter-icon">
                <div class="Icon Icon--twitter"></div>
              </div>
              <div class="tweet-author">
                <div class="TweetAuthor">
                  <a class="TweetAuthor-link" href="#channel">
                    {" "}
                  </a>
                  <span class="TweetAuthor-avatar">
                    <div class="Avatar"> </div>
                  </span>
                  <span class="TweetAuthor-name">Twitter</span>{" "}
                  <span class="TweetAuthor-screenName">@twitterofficial</span>
                </div>
              </div>
              <div class="tweet-text">BIG NEWS lol jk still Twitter</div>
              <div class="tweet-timestamp">
                <span class="tweet-timestamp-date">
                  Mon Apr 06 22:19:49 PDT 2009
                </span>
              </div>
            </div>
          </div>
        </div>

        <ResponsiveContainer width="30%" height={300}>
          <PieChart height={250}>
            <Pie
              data={data}
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
                    {data[index].name} ({value}%)
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

export default Analyzer;
