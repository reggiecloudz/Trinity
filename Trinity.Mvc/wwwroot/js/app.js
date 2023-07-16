function flashToast(data) {
    const flashToast = document.getElementById('flashToast');
    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(flashToast);
    const toastBody = document.getElementById('flashToastBody');
    toastBody.innerHTML = data;
    toastBootstrap.show()
}

function loadCities(stateId) {
    $('#CityId').empty();

    $.ajax({
        url: `/api/States/${stateId}/Cities`,
        success: (response) => {
            console.log(response);
            if (response != null && response != undefined && response.length > 0) {
                $('#CityId').attr('disabled', false);
                $('#CityId').append('<option value="-1">--Select City--</option>');
                $.each(response, (i, data) => {
                    $('#CityId').append(`<option value="${data.id}">${data.name}</option>`);
                });
            }
            else {
                $('#CityId').attr('disabled', true);
                $('#CityId').append('<option value="-1">--No Cities found--</option>');
            }
        },
        error: (error) => {
            alert(error);
        }
    })
}

function makeDonation() {
    if ($("#donation-form").valid()) {
        var myModalEl = document.getElementById('makeDonationModal');
        var modal = bootstrap.Modal.getInstance(myModalEl)
             
        var formData = $("#donation-form").serialize();
        console.log(formData);
        $.ajax({
            type: "POST",
            beforeSend: function (xhr) {  
                xhr.setRequestHeader("XSRF-TOKEN",  
                    $('input:hidden[name="__RequestVerificationToken"]').val());  
            },  
            url: "/Donations/Create",
            data: formData,
            success: function (response) {
                modal.hide();
                flashToast(response);
            },
            error: function (request, status, error) {
                modal.hide();
                flashToast(request.responseText);
            }

        });
    }
}

function saveProject() {
    if ($("#project-form").valid()) {
        var myModalEl = document.getElementById('saveProjectModal');
        var modal = bootstrap.Modal.getInstance(myModalEl)
             
        var formData = new FormData($("#project-form")[0]);
        formData.append("PhotoUpload", $("#PhotoUpload")[0].files[0]);
        // console.log(formData);
        $.ajax({
            type: "POST",
            beforeSend: function (xhr) {  
                xhr.setRequestHeader("XSRF-TOKEN",  
                    $('input:hidden[name="__RequestVerificationToken"]').val());  
            },  
            url: "/Projects/Create",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                modal.hide();
                document.getElementById("project-form").reset();
                // SELECT Name, COUNT(Name), StateId, COUNT(StateId) FROM Cities GROUP BY Name, StateId HAVING COUNT(Name) > 1 AND COUNT(StateId) > 1;
                flashToast(response.message);
                $("#projectData").append(`<tr>
                    <td scope="row">${response.name}</td>
                    <td>${response.cause}</td>
                    <td>
                        <a href='/Projects${response.id}/Details'>View</a>
                    </td>
                </tr>`)
            },
            error: function (request, status, error) {
                modal.hide();
                document.getElementById("project-form").reset();
                flashToast(request.responseText);
            }

        });
    }
}

function saveReward() {
    if ($("#reward-form").valid()) {
        var myModalEl = document.getElementById('saveRewardModal');
        var modal = bootstrap.Modal.getInstance(myModalEl)
             
        var formData = new FormData($("#reward-form")[0]);
        formData.append("PhotoUpload", $("#PhotoUpload")[0].files[0]);

        $.ajax({
            type: "POST",
            beforeSend: function (xhr) {  
                xhr.setRequestHeader("XSRF-TOKEN",  
                    $('input:hidden[name="__RequestVerificationToken"]').val());  
            },  
            url: "/Rewards/Create",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                modal.hide();
                document.getElementById("reward-form").reset();
                // SELECT Name, COUNT(Name), StateId, COUNT(StateId) FROM Cities GROUP BY Name, StateId HAVING COUNT(Name) > 1 AND COUNT(StateId) > 1;
                flashToast(response.message);
                $("#rewardData").append(`<tr>
                    <td scope="row">${response.item}</td>
                    <td>${response.quantity}</td>
                    <td>${response.amountNeeded}</td>
                    <td>
                        <a href='#'>View</a>
                    </td>
                </tr>`)
            },
            error: function (request, status, error) {
                modal.hide();
                document.getElementById("reward-form").reset();
                flashToast(request.responseText);
            }

        });
    }
}

function saveScene() {
    if ($("#scene-form").valid()) {
        var myModalEl = document.getElementById('saveSceneModal');
        var modal = bootstrap.Modal.getInstance(myModalEl)
             
        var formData = new FormData($("#scene-form")[0]);
        formData.append("PhotoUpload", $("#PhotoUpload")[0].files[0]);
        var lightbox = GLightbox({
            selector: 'data-glightbox',
        });

        $.ajax({
            type: "POST",
            beforeSend: function (xhr) {  
                xhr.setRequestHeader("XSRF-TOKEN",  
                    $('input:hidden[name="__RequestVerificationToken"]').val());  
            },  
            url: "/Scenes/Create",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                modal.hide();
                document.getElementById("scene-form").reset();
                // SELECT Name, COUNT(Name), StateId, COUNT(StateId) FROM Cities GROUP BY Name, StateId HAVING COUNT(Name) > 1 AND COUNT(StateId) > 1;
                flashToast(response.message);
                $("#sceneData").append(`<div class="col-sm-6 col-lg-3 position-relative">
                <div class="position-absolute bottom-0 mb-5 end-0">
                    <div class="dropdown mb-2 me-3">
                        <a href="#" class="icon-sm bg-primary text-white rounded-circle" id="photoActionEdit-${response.id}" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="bi bi-pencil-fill"></i>
                        </a>
                        <!-- Dropdown menu -->
                        <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="photoActionEdit-${response.id}">
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-tag fa-fw pe-1"></i> Remove Tag</a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-download fa-fw pe-1"></i> Download </a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-person fa-fw pe-1"></i> Make Profile Picture</a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-person-bounding-box fa-fw pe-1"></i> Make Cover Photo</a></li>
                            <li><hr class="dropdown-divider"></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-flag fa-fw pe-1"></i> Report photo</a></li>
                        </ul>
                    </div>
                </div>
                <a href="/media/scenes/${response.scenePhoto}" data-gallery="image-popup" data-glightbox="description: .custom-desc-${response.id}; descPosition: left;">
                    <img class="rounded img-fluid" src="/media/scenes/${response.scenePhoto}" alt="">
                </a>
                <ul class="nav nav-stack py-2 small">
                    <li class="nav-item">
                        <a class="nav-link" href="#!"> <i class="bi bi-heart-fill text-danger pe-1"></i>22k </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#!"> <i class="bi bi-chat-left-text-fill pe-1"></i>3k </a>
                    </li>
                </ul>
                <div class="glightbox-desc custom-desc-${response.id}">
                    <div class="d-flex align-items-center justify-content-between">
                        <div class="d-flex align-items-center">
                            <div class="avatar me-2">
                                <img class="avatar-img rounded-circle" src="/media/projects/${response.projectPhoto}" alt="">
                            </div>
                            <div>
                            <div class="nav nav-divider">
                                <h6 class="nav-item card-title mb-0">${response.projectName}</h6>
                                <span class="nav-item small"> 2hr</span>
                            </div>
                            <p class="mb-0 small">${response.projectManager}</p>
                            </div>
                        </div>
                        <div class="dropdown">
                            <a href="#" class="text-secondary btn btn-secondary-soft-hover py-1 px-2" id="cardFeedAction-${response.id}" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="bi bi-three-dots"></i>
                            </a>
                            <!-- Card feed action dropdown menu -->
                            <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="cardFeedAction-${response.id}">
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-bookmark fa-fw pe-2"></i>Save post</a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-person-x fa-fw pe-2"></i>Unfollow lori ferguson </a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-x-circle fa-fw pe-2"></i>Hide post</a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-slash-circle fa-fw pe-2"></i>Block</a></li>
                            <li><hr class="dropdown-divider"></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-flag fa-fw pe-2"></i>Report post</a></li>
                            </ul>
                        </div>
                    </div>
                    <h6 class="my-3 ">${response.title}</h6>
                    <p class="mb-0">${response.content} </p>
                    <ul class="nav nav-stack py-3 small">
                        <li class="nav-item">
                            <a class="nav-link active" href="#!"> <i class="bi bi-hand-thumbs-up-fill pe-1"></i>Liked (56)</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#!"> <i class="bi bi-chat-fill pe-1"></i>Comments (12)</a>
                        </li>
                        <li class="nav-item dropdown ms-auto">
                            <a class="nav-link mb-0" href="#" id="cardShareAction-${response.id}" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="bi bi-reply-fill fa-flip-horizontal pe-1"></i>Share (3)
                            </a>
                            <!-- Card share action dropdown menu -->
                            <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="cardShareAction-${response.id}">
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-envelope fa-fw pe-2"></i>Send via Direct Message</a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-bookmark-check fa-fw pe-2"></i>Bookmark </a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-link fa-fw pe-2"></i>Copy link to post</a></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-share fa-fw pe-2"></i>Share post via …</a></li>
                            <li><hr class="dropdown-divider"></li>
                            <li><a class="dropdown-item" href="#"> <i class="bi bi-pencil-square fa-fw pe-2"></i>Share to News Feed</a></li>
                            </ul>
                        </li>
                    </ul>
                    <ul class="comment-wrap list-unstyled ">
                        <li class="comment-item">
                            <div class="d-flex">
                            <!-- Avatar -->
                            <div class="avatar avatar-xs">
                                <img class="avatar-img rounded-circle" src="/assets/images/avatar/05.jpg" alt="">
                            </div>
                            <div class="ms-2">
                                <!-- Comment by -->
                                <div class="bg-light rounded-start-top-0 p-3 rounded">
                                <div class="d-flex justify-content-center">
                                    <div class="me-2">
                                    <h6 class="mb-1"> <a href="#!"> Frances Guerrero </a></h6>
                                    <p class="small mb-0">Removed demands expense account in outward tedious do.</p>
                                    </div>
                                    <small>5hr</small>
                                </div>
                                </div>
                                <!-- Comment react -->
                                <ul class="nav nav-divider py-2 small">
                                <li class="nav-item">
                                    <a class="nav-link" href="#!"> Like (3)</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="#!"> Reply</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="#!"> View 5 replies</a>
                                </li>
                                </ul>
                            </div>
                            </div>
                        </li>
                    </ul>
                </div>
                </div>`);
                lightbox.reload();
            },
            error: function (request, status, error) {
                modal.hide();
                document.getElementById("scene-form").reset();
                flashToast(request.responseText);
            }

        });
    }
}
