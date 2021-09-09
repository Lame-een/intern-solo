import './App.css';
import { Button, ButtonGroup } from 'reactstrap';
import React, { useState} from 'react';
import {
  Switch,
  Route,
  useHistory,
  Redirect
} from "react-router-dom";
import Song from "./Song";
import Album from "./Album";

export default function App() {

  var history = useHistory();
  const [pageButtonName, setButtonName] = useState("");

  function routedToPage(pageName) {
    if (pageName === "Song" && pageButtonName !== "Song") {
      setButtonName("Album");
    } else if (pageName === "Album" && pageButtonName !== "Album") {
      setButtonName("Song");
    }
  }

  function pageSelect() {
    if (pageButtonName === "Song") {
      history.push("/song");
      setButtonName("Album");
    } else if (pageButtonName === "Album") {
      history.push("/album");
      setButtonName("Song");
    }
  }

  return (
    <div>
      <header>
        <nav>
          <ButtonGroup>
            <Button name={pageButtonName} onClick={pageSelect}>{pageButtonName}</Button>
          </ButtonGroup>
        </nav>
      </header>

      <Switch>
        <Route path="/song">
          <Song callBack={routedToPage} />
        </Route>

        <Route exact path="/">
          <Redirect to="/song" />
        </Route>

        <Route path="/album">
          <Album callBack={routedToPage} />
        </Route>
      </Switch>
    </div>
  );
}