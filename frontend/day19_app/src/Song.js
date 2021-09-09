import { useState, useEffect } from "react";
import { Button, Table, ButtonGroup, Form, Collapse, FormGroup, Input, Label, Modal, ModalBody } from "reactstrap";

export default function Song({ callBack }) {

    const [songList, setSongs] = useState([]);
    const [albumList, setAlbums] = useState([]);
    const [isOpen, setOpen] = useState(false);
    const [modalOpen, setModal] = useState(false);
    const [serverResponse, setResponse] = useState("");

    useEffect(() => {
        callBack("Song");
        loadSongs();
    }, [callBack]);

    function modalTimeout() {
        let timerID = setTimeout(() => {
            setModal(false);
        }, 3000);
    }

    function loadSongs() {
        return fetch("https://localhost:44331/api/Song",
            {
                method: 'get',
                headers: new Headers({
                    "Authorization": ("Bearer " + "abs"),
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            })
            .then(res => res.json())
            .then(json => setSongs(json));
    }

    function loadAlbums() {
        return fetch("https://localhost:44331/api/Album",
            {
                method: 'get',
                headers: new Headers({
                    "Authorization": ("Bearer " + "abs"),
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            })
            .then(res => res.json())
            .then(json => setAlbums(json));
    }

    function submitSong(e) {
        e.preventDefault();
        let target = e.target;

        let songObject = {};
        songObject.Name = target[0].value;
        songObject.TrackNumber = target[1].value;
        songObject.AlbumID = target[2].value;
        console.log(JSON.stringify(songObject));

        console.log(e);

        fetch("https://localhost:44331/api/Song",
            {
                method: 'post',
                headers: new Headers({
                    "Authorization": ("Bearer " + "abs"),
                    'Content-Type': 'application/json'
                }),
                body: JSON.stringify(songObject)
            }).then(response => response.json())
            .then(data => setResponse(data)).then(() => { setOpen(false); setModal(true) });
        //.then(data => setResponse(data)).then(()=> setOpen(false)).then();
    }

    function renderTable() {
        let formattedList = songList.map((song) => {
            return (
                <tr key={song.songID}>
                    <td>{song.SongID.substring(0, 13)}</td>
                    <td>{song.Name}</td>
                    <td>{song.TrackNumber}</td>
                    <td>{song.AlbumID.substring(0, 13)}</td>
                </tr>
            );
        });

        return (
            <div>
                <Table key="table0">
                    <thead>
                        <tr key="headRow0">
                            <th>ID</th>
                            <th>Name</th>
                            <th>TrackNumber</th>
                            <th>AlbumID</th>
                        </tr>
                    </thead>
                    <tbody>
                        {formattedList}
                    </tbody>
                </Table>
            </div>
        );
    }

    function addSong() {
        let formattedSelect = albumList.map((album) => {
            return (
                <option key={album.AlbumID} value={album.AlbumID}>{album.Name}</option>
            );
        });

        return (
            <div>
                <Form onSubmit={submitSong}>
                    <FormGroup>
                        <Label for="songName">Song name</Label>
                        <Input type="text" name="songName" id="songName" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="trackNumber">Track #</Label>
                        <Input type="number" id="trackNumber" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="albumSelect">Album</Label>
                        <Input type="select" name="albumSelect" id="albumID">
                            {formattedSelect}
                        </Input>
                    </FormGroup>


                    <Button type="submit">Submit</Button>
                </Form>
            </div>
        );
    }

    function initializeForm() {
        if (!isOpen) {
            loadAlbums().then(() => setOpen(!isOpen));
        } else {
            setOpen(!isOpen)
        }


    }

    return (
        <div>
            <h1>SONG</h1>
            <ButtonGroup>
                <Button onClick={loadSongs}>Refresh song list</Button>
                <Button onClick={() => initializeForm()}>Add song</Button>
            </ButtonGroup>
            <Collapse isOpen={isOpen}>
                {addSong()}
            </Collapse>
            {songList ? renderTable() : ""}

            <Modal isOpen={modalOpen} onOpened={modalTimeout}>
                <ModalBody>
                    {serverResponse}
                </ModalBody>
            </Modal>
        </div>
    );
}